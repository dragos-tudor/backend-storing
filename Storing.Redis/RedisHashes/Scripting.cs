namespace Storing.Redis;

public static partial class RedisHashes
{
  const string AbsoluteExprKey = "absexp";
  const string SlidingExprKey = "sldexp";
  const string DataKey = "data";

  static readonly string GetHashScript = GenerateGetHashScript();
  static readonly string SetHashScript = GenerateSetHashScript();

  static string GenerateGetHashScript () => $@"
    local result = redis.call('HGETALL', KEYS[1])
    if(result[1] == nil) then
      return nil
    end

    local {AbsoluteExprKey} = result[2]
    local {SlidingExprKey} = result[4]
    if {SlidingExprKey} ~= '-1' then
      if {AbsoluteExprKey} ~= '-1' then
        redis.call('EXPIRE', KEYS[1], {AbsoluteExprKey} - ARGV[1])
      else
        redis.call('EXPIRE', KEYS[1], {SlidingExprKey})
      end
    end
    return result[6]";

  static string GenerateSetHashScript () => $@"
    redis.call('HSET', KEYS[1], '{AbsoluteExprKey}', ARGV[1], '{SlidingExprKey}', ARGV[2], '{DataKey}', ARGV[4])
    if ARGV[3] ~= '-1' then
      redis.call('EXPIRE', KEYS[1], ARGV[3])
    end
    return KEYS[1]";

}