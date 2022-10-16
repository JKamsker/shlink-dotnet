using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShlinkDotnet.Models.Http
{
    public enum OrderBy
    {
        [System.Runtime.Serialization.EnumMember(Value = @"longUrl-ASC")]
        LongUrlASC = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"longUrl-DESC")]
        LongUrlDESC = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"shortCode-ASC")]
        ShortCodeASC = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"shortCode-DESC")]
        ShortCodeDESC = 3,

        [System.Runtime.Serialization.EnumMember(Value = @"dateCreated-ASC")]
        DateCreatedASC = 4,

        [System.Runtime.Serialization.EnumMember(Value = @"dateCreated-DESC")]
        DateCreatedDESC = 5,

        [System.Runtime.Serialization.EnumMember(Value = @"visits-ASC")]
        VisitsASC = 6,

        [System.Runtime.Serialization.EnumMember(Value = @"visits-DESC")]
        VisitsDESC = 7,

        [System.Runtime.Serialization.EnumMember(Value = @"title-ASC")]
        TitleASC = 8,

        [System.Runtime.Serialization.EnumMember(Value = @"title-DESC")]
        TitleDESC = 9,

    }
}
