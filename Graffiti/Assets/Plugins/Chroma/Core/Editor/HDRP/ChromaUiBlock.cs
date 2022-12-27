#if CHROMA_HDRP
using Chroma;
using UnityEditor.Rendering;
using UnityEditor.Rendering.HighDefinition;

namespace UnityEditor {
class ChromaUiBlock : MaterialUIBlock {
    private readonly ChromaDrawers _drawers = new ChromaDrawers();
    private readonly ExpandableBit _foldoutBit;

    public ChromaUiBlock(ExpandableBit expandableBit) {
        _foldoutBit = expandableBit;
    }

    public override void LoadMaterialProperties() { }

    public override void OnGUI() {
        using (var mainFoldout = new MaterialHeaderScope("Exposed Properties", (uint)_foldoutBit, materialEditor)) {
            if (mainFoldout.expanded) {
                ChromaPropertyDrawer.DrawProperties(properties, materialEditor, _drawers, DefaultPropertyDrawerSg.Draw);
            }
        }
    }
}
}
#endif