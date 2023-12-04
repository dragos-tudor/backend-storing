namespace Storing.Redis;

public static partial class RedisHashes
{
  const string absoluteExprKey = "absexp";
  const string slidingExprKey = "sldexp";
  const string dataKey = "data";

  static readonly string getHashScript = GenerateGetHashScript();
  static readonly string setHashScript = GenerateSetHashScript();

  static string GenerateGetHashScript () => $@"
    local result = redis.call('HGETALL', KEYS[1])
    if(result[1] == nil) then
      return nil
    end

    local {absoluteExprKey} = result[2]
    local {slidingExprKey} = result[4]
    if {slidingExprKey} ~= '-1' then
      if {absoluteExprKey} ~= '-1' then
        redis.call('EXPIRE', KEYS[1], {absoluteExprKey} - ARGV[1])
      else
        redis.call('EXPIRE', KEYS[1], {slidingExprKey})
      end
    end
    return result[6]";

  static string GenerateSetHashScript () => $@"
    redis.call('HSET', KEYS[1], '{absoluteExprKey}', ARGV[1], '{slidingExprKey}', ARGV[2], '{dataKey}', ARGV[4])
    if ARGV[3] ~= '-1' then
      redis.call('EXPIRE', KEYS[1], ARGV[3])
    end
    return KEYS[1]";

}