using nobnak.Gist;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeSyncSys {

	[ExecuteAlways]
	public class WeSyncFollower : MonoBehaviour {

		protected Validator init = new Validator();
		protected IWeSync we;

		#region unity
		private void Update() {
			init.Validate();
		}
		private void OnEnable() {
			#region validation
			init.Reset();
			init.Validation += () => {
				if (we == null) return;
				var sub = we.Space.CurrSubspace;
				transform.localPosition = -sub.localField.center;
			};
			#endregion
		}
		private void OnValidate() {
			init.Invalidate();
		}
		#endregion

		#region methods
		public void Listen(IWeSync we) {
			this.we = we;
			init.Invalidate();
		}
		#endregion
	}
}