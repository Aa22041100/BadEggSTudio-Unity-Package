using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadEggStudio.Core.Exception;

namespace BadEggStudio.Core {
	public class Api {
		Http http;
		string urlPrefix = "<YOUR_API_URL_HERE>";

		#region Constructor
		/// <summary>
		/// Contructor for Api Object. It is used to init the url and get the Api object.
		/// </summary>
		/// <param name="urlPrefix">The target API url.</param>
		public Api (string urlPrefix) {
			http = new Http ();
			this.urlPrefix = urlPrefix;
		}
		#endregion

		#region Get Contact Us Response
		public IEnumerator GetContactUs (Action<string> OnGetContactUsResponse) {
			yield return http.Get (urlPrefix + "/info/contact_us", OnGetContactUsResponse);
		}
		#endregion

		#region Get Promotion
		public IEnumerator GetPromotion (Action<string> OnPromotionResponse) {
			yield return http.Get (urlPrefix + "/promotions", OnPromotionResponse);
		}
		#endregion
	}
}
