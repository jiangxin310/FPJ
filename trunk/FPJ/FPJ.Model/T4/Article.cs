//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 2016-08-24 17:10:47
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;
namespace FPJ.Model.Test2
{   
		 /// <summary>
		///实体-Article 
		/// <summary>
		public partial class Article
		{
			
				 /// <summary>
				///  
				/// </summary>
				public int  Id {get;set;}
				
				 /// <summary>
				///  
				/// </summary>
				public string  Title {get;set;}
				
				 /// <summary>
				///  
				/// </summary>
				public string  Content {get;set;}
				
				 /// <summary>
				///  
				/// </summary>
				public DateTime  CreateTime {get;set;}
								
		}
	
		public partial class ArticleMapper:ClassMapper<Article>
		{
			public ArticleMapper()
			{
        			Map(a=>a.Id).Key(KeyType.Identity);
          			AutoMap();
			}
		}
}
