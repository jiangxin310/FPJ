//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 2016-08-02 17:36:57
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;
namespace FPJ.Model.Default
{   
		 /// <summary>
		///实体-User 
		/// <summary>
		public partial class User
		{
			
				 /// <summary>
				///  
				/// </summary>
				public int  UserId {get;set;}
				
				 /// <summary>
				///  
				/// </summary>
				public string  UserName {get;set;}
				
				 /// <summary>
				///  
				/// </summary>
				public string  Address {get;set;}
				
				 /// <summary>
				///  
				/// </summary>
				public int ? Age {get;set;}
								
		}
	
		public partial class UserMapper:ClassMapper<User>
		{
			public UserMapper()
			{
        			Map(a=>a.UserId).Key(KeyType.Identity);
          			AutoMap();
			}
		}
}
