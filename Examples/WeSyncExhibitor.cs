using ModelDrivenGUISystem;
using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using nobnak.Gist;
using nobnak.Gist.Cameras;
using nobnak.Gist.Exhibitor;
using UnityEngine;
using UnityEngine.Events;

namespace WeSyncSys {

	[ExecuteAlways]
	public class WeSyncExhibitor : AbstractExhibitor {

		[SerializeField] protected Links links = new();

		protected WeSyncBase.Tuner tuner = new();
		protected BaseView view;

		#region unity
		private void OnEnable() {
			var we = links.we;
			if (we == null) {
				Debug.LogWarning($"We sync not found");
			}
			ReflectChangeOf(MVVMComponent.Model);
		}
		#endregion

		#region Exhibitor
		public override void Draw() {
			if (view == null) {
				var f = new SimpleViewFactory();
				view = ClassConfigurator.GenerateClassView(new BaseValue<object>(tuner), f);
			}
			view.Draw();
		}
		public override void ResetView() {
			if (view != null) {
				view.Dispose();
				view = null;
			}
		}
		public override void ApplyViewModelToModel() {
			var we = links.we;
			if (we != null) {
				we.CurrTuner = tuner;
			}
		}
		public override void ResetViewModelFromModel() {
			var we = links.we;
			if (we != null) {
				tuner = we.CurrTuner;
			}
		}

		public override string SerializeToJson() {
			return JsonUtility.ToJson(tuner, true);
		}

		public override void DeserializeFromJson(string json) {
			JsonUtility.FromJsonOverwrite(json, tuner);
			ReflectChangeOf(MVVMComponent.ViewModel);
		}

		public override object RawData() => tuner;
		#endregion

		#region declarations
		[System.Serializable]
		public class Links {
			public WeSyncBase we;
		}
		#endregion
	}
}
