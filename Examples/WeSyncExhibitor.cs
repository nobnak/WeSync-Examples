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
	public class WeSyncExhibitor : WeSyncBase {

		protected BaseView view;

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
			validator.Invalidate();
		}
		public override void ResetViewModelFromModel() {
		}
		#endregion
	}
}
