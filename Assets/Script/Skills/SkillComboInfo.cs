using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillComboInfo", menuName = "Skill Combos")]
public class SkillComboInfo : ScriptableObject
{
    [System.Serializable]
    public class SkillCombo
    {
        public Elements.ElementalAttribute ElementOne;
        public Elements.ElementalAttribute ElementTwo;
        public bool NeedQuaternion; 
        public GameObject prefab;
    }

    public List<SkillCombo> _SkillCombos = new List<SkillCombo>();

    public GameObject GetPrefabForCombo(Elements.ElementalAttribute selfElement, Elements.ElementalAttribute teamElement)
    {
        foreach (var skill in _SkillCombos)
        { 
            if (skill.ElementOne == selfElement && skill.ElementTwo == teamElement)
            { 
                return skill.prefab;
            }
        }
        return null;
    }
}
