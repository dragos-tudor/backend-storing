using static Storing.Redis.RedisExpirations;

namespace Storing.Redis;

public static partial class RedisHashes {

  static readonly string hashGetScript = GenerateHashGetScript();

  static readonly string hashSetScript = GenerateHashSetScript();

  static string GenerateHashGetScript () => $@"
    local result = redis.call('HGETALL', KEYS[1])
    if(result[1] == nil) then
      return nil
    end

    local {absExprKey} = result[2]
    local {sldExprKey} = result[4]
    if {sldExprKey} ~= '-1' then
      if {absExprKey} ~= '-1' then
        redis.call('EXPIRE', KEYS[1], {absExprKey} - ARGV[1])
      else
        redis.call('EXPIRE', KEYS[1], {sldExprKey})
      end
    end
    return result[6]";

  static string GenerateHashSetScript () => $@"
    redis.call('HSET', KEYS[1], '{absExprKey}', ARGV[1], '{sldExprKey}', ARGV[2], '{dataKey}', ARGV[4])
    if ARGV[3] ~= '-1' then
      redis.call('EXPIRE', KEYS[1], ARGV[3])
    end
    return KEYS[1]";

}