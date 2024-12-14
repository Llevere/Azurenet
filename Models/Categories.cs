using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Azurenet.Models
{
    public enum Categories
    {
        [EnumMember(Value = "Test Category")]
        Test,

        [EnumMember(Value = "Random Category")]
        RandomCategory
    }
}
