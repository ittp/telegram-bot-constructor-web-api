using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Utils
{
	public static class JsonResultExtentions
	{
		private static T GetPropertyOfAnonimusObject<T>(object anon, string propertity)
		{
			return (T) anon.GetType().GetProperty(propertity).GetValue(anon, null);
		}

		public static T GetPropertyOfJsonResult<T>(this JsonResult result, string property)
		{
			return GetPropertyOfAnonimusObject<T>(result.Value, property);
		}

		public static T GetPropertyOfFirstElementInJsonResult<T>(this JsonResult result, string property)
		{
			var element = ((IEnumerable<object>) result.Value).FirstOrDefault();

			return GetPropertyOfAnonimusObject<T>(element, property);
		}
	}
}