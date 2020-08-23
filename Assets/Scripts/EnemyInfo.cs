using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    public Sprite[] enemySprites;
    public string[] enemyNames;
    public string[] enemyDescriptions;

    public GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
        enemyNames = new string[10];
        enemyDescriptions = new string[10];

        enemyNames[0] = "Goblin";
        enemyNames[1] = "Orc";
        enemyNames[2] = "Gargoyle";
        enemyNames[3] = "Leech";
        enemyNames[4] = "Goblin Shaman";
        enemyNames[5] = "Big Orc";
        enemyNames[6] = "Big Gargoyle";
        enemyNames[7] = "Big Leech";
        enemyNames[8] = "Big Goblin Shaman";
        enemyNames[9] = "Goblin King";

        enemyDescriptions[0] = "Simple, weak enemies. Although very primitive, they are very fast and will often " +
            "team up together with other mobs to pose a bigger threat.\n\nEXP Yield: Low\nFloors: 1+";
        enemyDescriptions[1] = "Big enemies with a slow attack. Their attacks are often very telegraphed and can" +
            "be seen by their axes flashing. Although slow, they have a lot of health and attack.\n\nEXP Yield: Medium\nFloors: 1+";
        enemyDescriptions[2] = "These mobs will start off slow, but speed up the longer they haven't hit you or been hit. Their" +
            "attack range can be very small, but can pack quite a punch. Really hard to kite around.\n\nEXP Yield: Medium\nFloors: 2+";
        enemyDescriptions[3] = "These enemies will inflict the Infection debuff that reduces STR by 20% when they hit you. " +
            "Their slime also surrounds them, so standing close to them will also deal damage to you. Hitting " +
            "these enemies in their head or tail will not actually damage them.\n\nEXP Yield: Medium\nFloors: 3+";
        enemyDescriptions[4] = "Always surrounded by other goblins, the shamans will throw fireballs that deal high damage and will " +
            "continue until they hit you or a wall. They however have very little defenses.\n\nEXP Yield: Medium\nFloors: 4+";
        enemyDescriptions[5] = "A bigger version of the Orc enemy. Same patterns, but watch out for his attacks which " +
            "can cover a very large area.\nEXP Yield: High\n\nFloors: 1-4";
        enemyDescriptions[6] = "A bigger version of the Gargoyle enemy. Same patterns, but watch out for his attacks which " +
            "are now a higher range. This boss will also move very fast if you leave it alone.\n\nEXP Yield: High\nFloors: 1-4";
        enemyDescriptions[7] = "A bigger version of the Leech enemy. The head and tail of this enemy will not take damage" +
            " so keep that in mind when fighting this boss. Try to dodge his attacks to avoid the debuff.\n\nEXP Yield: High\nFloors: 1-4";
        enemyDescriptions[8] = "A bigger version of the Goblin Shaman enemy. The big shaman has less recharge between " +
            "fireballs than normal shamans. Also watch out for the goblins that are in this encounter, which total 4." +
            "\n\nEXP Yield: High\nFloors: 1-4";
        enemyDescriptions[9] = "The floor 5 boss, which has 2 phases, the first phase ending when you kill all the goblins in the room." +
            "In the first phase he will summon and buff goblins while having increased DEF. In Phase 2, he will charge at you headfirst" +
            "and attempt to kill you himself, while increasing his attack.\n\nEXP Yield: Very High\nFloors: 5";
    }

    public void UpdateUI(int toUpdate)
    {
        tooltip.transform.GetChild(0).GetComponent<Image>().sprite = enemySprites[toUpdate];
        tooltip.transform.GetChild(1).GetComponent<Text>().text = enemyNames[toUpdate];
        tooltip.transform.GetChild(2).GetComponent<Text>().text = enemyDescriptions[toUpdate];
    }
}
