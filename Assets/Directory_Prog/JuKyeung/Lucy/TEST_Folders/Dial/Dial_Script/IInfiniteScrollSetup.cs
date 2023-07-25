using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInfiniteScrollSetup
{
	void OnPostSetupItems();
	void OnUpdateItem(int itemCount, GameObject obj);
}
