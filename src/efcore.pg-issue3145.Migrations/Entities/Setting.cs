using System.ComponentModel.DataAnnotations;

namespace Migrations.Entities
{
    public class Setting
    {
        public long Id { get; private set; }
        public string Name { get; set; }
        public string Value { get; set; }

#if Net8
        [Timestamp]
        public uint Version { get; private set; }
#endif

    }
}