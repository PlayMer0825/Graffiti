using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insomnia{
	[CreateAssetMenu(menuName = "ScriptableObject/Quest/Action/SimpleIncrease", fileName = "Simpleincrease")]
	public class SimpleIncrease : ScriptableObject {
		public void Progress(int inc, ref int counter, in int max) {
			counter = Mathf.Clamp(counter + inc, 0, max);
		}
	}
}
