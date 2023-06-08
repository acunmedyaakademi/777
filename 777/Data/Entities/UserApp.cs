﻿using _777.Data.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace _777.Data.Entities
{
    public class UserApp : IdentityUser<int>, IBaseClass
    {
        public DateTime CreatedOn { get ; set ; }
        public DateTime UpdatedOn { get ; set ; }
        public bool IsActive { get ; set ; }
       public  List<Text> Texts { get ; set ; }
    }
}
