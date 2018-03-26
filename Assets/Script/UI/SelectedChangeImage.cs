using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

  public class SelectedChangeImage : MonoBehaviour {

    public void ChangeImage()
    {
        switch (this.name)
        {
            case "Save":
                GameObject.Find("Save").GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_save_W", typeof(Sprite)) as Sprite;
                break;
            case "Load":
                GameObject.Find("Load").GetComponent<Image>().sprite = Resources.Load("UI/Menu/c_load_W", typeof(Sprite)) as Sprite;
                break;
            case "Music":
                GameObject.Find("Music").GetComponent<Image>().sprite = Resources.Load("UI/Menu/d_music_W", typeof(Sprite)) as Sprite;
                break;
            case "Sound":
                GameObject.Find("Sound").GetComponent<Image>().sprite = Resources.Load("UI/Menu/d_sound_W", typeof(Sprite)) as Sprite;
                break;
        }
    }
  }

