using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;

public enum SpellType {
    None, Arcane, Fire, Water, Earth, Air, Dark, Light
}

public interface ISpell {

    string GestureId { get; set; }
    int ManaCost { get; set; }
    SpellType SpellType { get; }
    string Name { get; set; }
    string Description { get; set; }

    void AddSpellToCast();
    void DoSpellEffect(RecognitionResult result);
}
