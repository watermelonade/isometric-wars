using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour
{
	/**
	 * Class to organize 'stage' data for the game.
	 * Stores information about where to get the map, 
	 * when cutscenes happen, which cutscenes happen, 
	 * and which enemies and items are present.
	 * Maybe it should be the one to create the map
	 * and cutscene objects directly, and give them
	 * to the Battle and Cutscene managers, respectively?
	 */

	/** 
	 * Member Variables
	 * 
	 * mapFiles[] - needs to have a file to get the map from,
	 * and could potentially be multiple files for complex stages
	 * 
	 * cutSceneFiles[] - needs to have a file to get the cutscene from,
	 * and is likely to be multiple files due to the nature of the game
	 * 
	 */


	/**
	 * Methods
	 * 
	 * getMap() - by default gives the first/only map for the stage
	 * getMap(int number) - overridden to select a different map
	 * 
	 * getCutScene() - get first cutscene
	 * getCutScene(int number) - as above
	 * 
	 * alternatively/additionally could do
	 * nextMap() and
	 * nextCutScene()
	 *
	 */

	/**
	 * This also raises the question of who knows what cutscene comes next?
	 * The map might know because it might be based on reaching a certain tile.
	 * The stage might know: why?
	 * I don't really like the map knowing about cutscenes, what is the alternative?
	 */
	
}