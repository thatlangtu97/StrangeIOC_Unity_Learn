using UnityEngine;
using strange.extensions.context.impl;

namespace EntrySystem {
	public class EntryContextView : ContextView {
		void Start()
		{
			DontDestroyOnLoad(gameObject);
			context = new EntryContext(this, true);
			context.Start();
			
		}
	}
}
