using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour {
    public GameObject LoadingCanvas;
    public GameObject PlayerPrefab, WolfPrefab, EnemyPrefab;
    public GameObject Wolf;
    public List<int> WolfState;
    public List<Vector3> WolfVector3;
    public List<Quaternion> WolfQuaternion;
    public List<int> EnemyState;
    public List<Vector3> EnemyVector3;
    public List<Quaternion> EnemyQuaternion;
    public string PlayerState;
    public Vector3 PlayerVector3;
    public Quaternion PlayerQuaternion;
    public float SaveRotx, SaveRoty;
    public Slider LoadingSlider;
    private AsyncOperation _async;
    public string LoadSelectedData;
    private CameraScript CameraScript;

    // Use this for initialization
    void Awake () {
        SpawnAllObject();
    }
    private void Start()
    {
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        CameraScript.rotX = SaveRotx;
        CameraScript.rotY = SaveRoty;
    }

    // Update is called once per frame
    void Update () {

    }

    public void LoadSence()
    {
        Instantiate(LoadingCanvas, Vector2.zero, Quaternion.identity).name = "LoadingCanvas";
        StartCoroutine(LoadLevelWithBar("Game"));
    }

    IEnumerator LoadLevelWithBar(string level)
    {
        _async = Application.LoadLevelAsync(level);
        while (!_async.isDone)
        {
            LoadingSlider.value = _async.progress;
            yield return null;
        }            

    }
    public  void SpawnAllObject()
    {
        if (File.Exists(Application.persistentDataPath + @"\Save\NewGame.sav"))
            LoadSelectedData = GameObject.Find("ChooseSaveData").GetComponent<ChooseSaveData>().SelectedData;
        else
        {
            LoadSelectedData = "NewGame";//測試用
            StreamWriter streamWriter = File.CreateText(Application.persistentDataPath + @"\Save\NewGame.sav");
            streamWriter.Write("fBq6B6h7yLNxunrIOTOwW12tq7Wp2Dz6su3gXtp4ck+C17fkppANr5FsByNHMyp0x1BxkNrRQqIZESIHxF+LH3aPiOz/Me7IW6XTQh5b9LP0rGBlZuuBS6mUjCMHlnUU5VoTMTNRJUSfyraBdi2v8CzSzhgLO+Rsqd0CA89yVWHgWipH+QbRJ8XWyKfgpF6KbKCz/roVxwADNkispoQZR7au2Rw2yJoRNVi62xMQmDF4CjBMp4WobSLDGlIRsWSV06jlQLNbD8FIi6UywWfHF4iyhU0jF59+4F/MvjRcQCE2Tv1T9wNV0s3OVuoDtmZcTtQNEYMoU//0D9AFe5t4knbx42JoW/tbrwJk26BWoi8BDkFAswFNSw5BXZX8bq+6T+kpVaScGfBZ6QgZR/2wgwAvjGVmMeXHXDT5TPZh9CqUD308QL/akgC58bClB/ryzC2PDmU36NMB8gIZYQDLrrXditjhEYGCMrtmawM/KRC1e3yAxZq+x/jMR26napGcEHZ4VtbS7Gb9c5fuqdW/ICSwK2s26iaLQChLH5yP4Y1CZw+vMNOIS9acfbLVDudjwYRfQGHjH3KMjunixvbuxRBvODczopx4GnDqemhyLUYs+Tz0ZA6edOM1I5n3baxcPJiTEA+b9sE/qQmdE1E2CS4I3b6oxfYTgRIc2rBkSwIAf4G9vyJRK6d7+F66bmD8gbGtgjLCzXXS3kEa7n4CszweksjSQlvYV7Qy3StavxK2LnVYLtk9uzir9KZuaCGK4WVTBNiHWupGU1Ltfc6TERcmTsaGeIpfwPOojihatR1L8WNpEb4M2cDpiMXeyGbeKTCUaFSVe+Moi/ucIj1CenG7V+pCsyAgXPl10Yao2QWUkqyiPSZr46OE7pvz+3q4NjbIlSMqaQ4KsTESCA8zDAXTOfeL7WOAWVvUSyfjKlozBnnKn2Blwlf7bv+etXFTKUKW5Kpf3j7NV4gN7LiizRy1gCBdIDGJjo5HCX1Eo3OG+0L8GmkRdCUFCDxZSD9Crcompw7ePvYYo+z/+i0MriibhvmovKymVceaPObcHsMSOG27u2gFp8j/YCrCJX5/OdUpNhmJJpKN7lSlrEpjMi3n9bTGNUn9fcGaHDi84jefbqF89+8V/6kqWrrp9s206TV7hrb4AJh87c0G6UYarhm3YrJAmxMwYfZlY29pY8rSiTvyURv37omPbwCBCcDuBosIh4/JzpRHlTSLiBH/nVE/Q9obpmAiZgnPMbTh+Squkze87ECW2rAI1q0OHSPB/GB5w4zZ/krGwfJoA7Ea4zKu28pnijI1wJ0l66gg4CVmPpchyGKUB6V3UHJZDS8w3IIz5dfIv3da8iIEwsM4jWTlznF67CrsEcnQNc0mK+3kQMbXQIS7kTfxtVr8+9m2r5oL8ho/7pIWrYD8VmynfAyMc78zWK8kuE/GIU59t09cIXHYFeAI/nQ66bbCJQfzDkoI2FmUs9HSmyCMp37/NwFQOCCoL2upkJqzv56Y7j6CUlxwo1W9S5Ks02wTqjNJVRBuokdxZ5xAnGjgojjn9+KWnvX09CtaNydJPD/evbAGDB1pa/IXXGbn2QagTs215o4wbCM+mdZ45IAKd5OqPoUNgejo8B5BhAgK/cHt0wQojUjnxh2k4M4pjaPiHgsspP2rK987f4BfYB/uzhAiA9SMrlBB4pClkXhHIaQ1Xj0uS4mpaFXWseO3E5ceEv8yFa0YI4LnRfRwQQL9kTOO5IoXNXUs7kgnNtm04nNvIboUOlYTmYZDJ4DUVP5Du9o8NBY5KrW0jFGvufK+Nd+H1M4V7QABF4cvkfmTSeL3oenna3D7kVUYgfKZ7hmJ1i4q8U8uUDlOocXTJQEEK4Bjkp+aoZHJWYXRZ3qxrp8qIr28M0SsZ3avy897kHPtFUKAbiFPkDW5tOCjULMIWmB/aQLGidehf1hRvmebPgrLiJMjK2z1M2f/GK0SHUKeu+qhiv0GqODw/XYYKOKH6KZyhQ33+H2cUVBQCbDfFWk2VILyAsCozOp9hfyU4Fl1udJAKVEYEpwN2/9rvoXCZWoEKdFMmqIRz+SbP8n6SRkIPlL84g765t77i2pL7AIU2TB1bKP0Juwhdd2Z9U9wohGlURKaocbuDdYokKDtJNaub46p1YNmM5n2acjQj2r2hbMN2DEtq4SVyoq/KNDizzB+u34Cxr7JIMlnXS2Fpcobd+rzmRSG1NQKq+sW26M2XkOjgxe+xad7PDoQfGtV24q0HNSWCadH3NiQHOjjNa+lC05/zdbkOo8WWlYBQDfxrzziwHEcuMopFOx0TgRmh9AF3f5VO0wYkyFnFYZvOEoJxo8bYFTQagb0KeiGdxLVNO/3biSCxZ6gdYHUXKZEsGGChp6/FeJr26rNiieXDtrl+D0VULCyt00D2+1JQE9dEwW4KFdD7aNAiSWQD7H6DSrd9jvmZVaglZrvqL5JluBGmqqfAa8DzWmpMULXkRB3Z39sigJ+MGtRdwBxCJsyqYo9iHQ161ppy8WkgwzVbRlFEW/PHZnVzzj7Gm7fGUgZG9U7+FBN+BI/qr1HY5CfiKYJVcKh1e30YdKn6wi+tONYavWl/uQxrWGiUqc+RKaFQayduJBPu0IE2KPhLvxISyb1qXbt55Uf2oDpadUiFGhgBSktKKFCUBEANaYXHPJBRLz/MtYZcUKuONOL6k72E8eNyGOmEqQV0OJ1SApT6+fZfmA7oSILDhvNIFWJIEmI7VopVYjz4ZkrHbgBFulkqYLgtmWLYddQZb2Jfo4io5dBJjDusR/W7lsZEMX6ZMuclb9mvyUY44SGwZm+lb5XxkTzafziDvrm3vuLakvsAhTZMHVwgj8IBcjyiyEVzRYoPKasBvwkeK09K4YXYFslpMVy2izDMDngftqmKjZxJD9burJZ0R1vGTOMa3pvVtT8tDRwQwzRgv+6izHhsYCK2a+fQCSniNYG4/zEtlmBBE9+6eWSM0LFyLVYEFRhT1DhlUJkrItBZFv38a30iG1l6ifSctN60EM5NaCDDK8LEY9FJ7BCqjUh57j/cqXIP6XMXuLY2hHTcs9Yn4TKL2wWWHwoNLbFiq/0yUPMDEl85/YWF8O6gbH2LsV04+rjwnzcyxXv9SmgHj6nlh3BgEBIz3fVWhZjehlV+Dd5Dnj0Mq9Ybm4NUqAD6gDE+nbLEgVutZSMN5lhdC1pmHbpwS8xZky0ybALelLx6ZN6hMRfp7YP3AAEgRjbY37OKOeLd3SelNJExUlbdIuxvhZGJX+JMz+DqZoHIeHwdKG2pMkU1EXSNt61OpFL0zXvV6JCkWlH+Z2FRVi19cMgdZA+jFCF8UdBKwefFU2nbjqZtKTNCUECsf3Y4pzog4Penm+5wILAmlBUCZ3lvp23Sp5rCFGu7Rcwp63XI6Rtmz4Ws7WY3+0FE1Gy2OeNUtX4FMxayiMd/a40YkiDOg7PhyNxSvd372S/LWbYnyPEs6gdtX+0sIz0YIZ+8mLK7JVpeqFJlFKj+d09kWpKaJYTTp2dP/JE94e3g1j6XKHBazxBnWLq2U1Jh711vR8fNiMCbkOKjSO6yu59LpVCXL4kKGdOPoca+K8fxrR4Ys17ZzsrTv25v43WiXEpmEl1lEwPXZ9SFOZMc+akMkzJoQfvrkglUF1+SzXbPSCrElQsLy96Jtra+D8rfuQT30R6x9daMUNq/fHPXsUwTqYw/a8qPCFvWZxfHcr2n7vosro0G5reLPfaHfsivQ7Ywm9tHeBcfoVZ7PABW1B2Tt0+0zRsZTucNN6ygRJtvjRhmlfvquWGUUwUjyNG2ZnGzBUpgnYani1TWY3EDiRyTegPeeZ7k9L0PxPSEu6bahcfTbtox7AVbjl2fjWHVL6qRGdQSLPF7K4EQ+FTcuPexfhvdDsjLAUw0rcjMM9dH3/N1uQ6jxZaVgFAN/GvPOKl/dSFffDcbWKGqf0Cy2YuoRlUDs3D3ER3TJTCMGv4bi7hU3K/Gdx4eDOCD5H31felCCNpYvv9UNOWzX5VFavdAfNjHiya7LOnopm++bLL2l9Z+KKmSqv8YtFYqvNDIkbdC4+U+N8F4gpo/BgQBGYOuhZKExeXSnCKnauVPLNdzW90cnK2OEWCirIRRObZb6QbqA95XlO6MudL0iTBEwGmAjD+kgMUaFBDI0H19TMr4Bbe0Z/2iDmQp94v/YYYY60zliWMnBub259UZk0hWhIBsNtPs6Q2oEcOZhrRK/uoKH0CXzAojVOrS3o+Ivyoq/4soiy0Yd+/K3dSvf2H7MTxYIswrJbp9Zq2SjBJTm0Qas7KWVFh3gXMDlrbNMCbJpZu2Q5AroGUGnpNIPno82Sqn+dsTHpwSR7JTQoMVeOdbEFkARuIMNQegt46lfBXuEOWF5DzRbxnL2qOEafkLyS8phdJv1RVd9B5XQZwnSyZSY9Lu6Dj1hmDj7lNJU3GKk7IZ+md9e9jYmSjqGNfHv2TNBHzwvMQs+dUZzM1wu4DR3Lz9uyhuobeP0QpK1hfOQFZHVnVFiBPHlm1DOystinizdk13ryTYq/l4u7FnwSCIbHVjOhYfNxOLQQD3uAN5bEwMm/XcW9YrOZp7qWaAi47gjI3sLB73iVdYAgunDZNgpSTlhdo3KmV0KEQ6h1CNp3tfWeMg0MX7TDNaX4IzFFE7r8WWi48gmvaJXje839l5HuBg3BX++6ytULrCEmPDGynljtI3fGED41H9EgcV7FwtL+TqIb7FObejC6SklJ+KILUzM4EL9HgtIhmBfnorWtuEx/ZJFRPD386avIf9z/dvC/WN6b4OEfC1CekhcgCcsW/s41gNA0YUlxo5laOQ2MsN6Awj2sDUVpx/Km1iGgCY+Q5i31qv8CbnOL1K5/2/UKpGHyEtL5TE7URgEhp9J8R5KGj09QZgiXbkUzYMrBuiAGCtbPVGps2Bzv1aKU+sNxafCGEJY89swNz0CaUr2yzGIPh4jfhPnSModmC7O868T31WiG3VDMYIgdLHdR3BYcOaT/2UtGstG1a0i3uC89/UbVIkAxw+MAcBm8x46S41cALVLaxfeVIs+uVdpYhn6nL/gGgT0BFZhSzWoB6Op3e3YHG93/SFfUBY/emT0CXEsqah8UPF0hajslrSQjAy9++jRgIBn/8L1kOpVkVDEEFqK+wqxxBIg1/XLQGkY+NwRTirHNyAEr1+M+ah7T9DfYEHPSsaB7WQlcFWfCEuO0ALqxZJhI8pn9Lk/ShXXyXnWkhtdJj0Iq+dokwJ0NiUptc/6AE5/IaRRYPO/i1BzXjtElodAlXWVa8/387t8r1ZOXOcXrsKuwRydA1zSYr7VNavA9YcFSamA5DSq8ec2S4cei4ed0x2pdHjNFVC7qFJOrFMH3aGb2wQxxBcoqFmFZEB/LTM6Nf5lo1J23Z47FHfK1zBCk89rhzxl1umGHu+VSwLVXl24cQviN4s8UlDEkBdVRnD3Ar59RxF2XF6664cei4ed0x2pdHjNFVC7qFsEiCD7IiMLT21weB+NO2sYBosfRiyiFCrrGseijTD1dcHASmaLLoQbLiaSN/VTnXZOXOcXrsKuwRydA1zSYr7X5Cqq+kY2s4++msk44FDQm4cei4ed0x2pdHjNFVC7qFhmV49+VjkjI4QypC1imo98KVOnBzzSKMaM0Ej/YQhgv9sgr3n/9WcAyN62W3naYruOce9aGjvUACxgutnivUF8WMQEKP9hE1JWZmUC13pGR9alYDQyTJoJak1JfuD5rFnzhkgYCXCE9vmhsXvSyGc3BGa5dwvyAouZAAnVDZ89nq2kny09u1zOg1eWFDcJKZuOce9aGjvUACxgutnivUF+w51k3RLWMCVSX+6mCgV753RTo/hwTkhEVoHg7E/XS/prMUCQ4afJvN2m2FPj7yajbkA6cUK+fGfqN3RrWbonOm0tnxSy5id2nsvHnqxsVbFd0YFWsEoseIQEyQXZznPnzNs1ZCTkmKiOmaEF+c7wB02kJumvd/WqmGYYuzenIyra2H43xt+E4AQrmDYYK/cbzZGhHhXCVTsFw7RBckxz/KBswLILCWeDd7E1QOOlG5ks+lMLOWh5Of2h/yo8ZjlrP+lFqDQ6KYuq1NCQpV1akC1i610Re9g3BLvrDCNMi0hZiZ1MF1cdl87XGHkJfoPirE9u2qipDg/RjJtlOkMtQoxuPZ8HTTWxCKRGZrlzt5sh1VzCH/gva1JP6vfDUkVoBmnzLkL3JHZLm4y9tBBCu2tO6Vevzf/ljJLtpKTziK6Gww1rkADb6trVAWcbOcKP5G1UIVgKvWSWisg0HQ2PYdCYLizRMeVSBM7XXiG/Xf197pyLtfnEmuzc2Q9qAVnUtTLrn+MxGKtrPmkQESg5S2tO6Vevzf/ljJLtpKTziKHlZ/XGguA7Two5nNAOjULn7eDJOZ1XhDEMP020u0+icnRdQm+FU8qoYDVX5EBdVjv6f5hOxAzhzQliGIcfaGC2GWHMLF8Jgt3IpRu/4na2PqpR+Aa+hpR1UVTHZV2Uu6eRl7YtidLfKq/Pun9E9hzZckknYPX49yO/CceFg7jdGuc/bROzlKvFdrYsbR9eD0UeqYPNl55g2v9sLz+XO5mpWpB7QUoTA/d41KL+3IPwaxyO6d2BFznhtN9nv/jrHJ6wYb6Ri14FU80zxdyROAPH7Efcsr7pgFoiu3hu9rh2vYwm9tHeBcfoVZ7PABW1B2ohJtWKKSm40buoOIof5AsQTHoWExM4xhOlzcw1u44Me0748+6hiQ8uDsWxQy8yXcjxtyXasTL0WcVdDActasZ01QJ9/ISK0A0KTuhmDlFkrchqO6IxHbF/7uAePD8iWH6rU/we16ZUdXe24c0/i/Yd+hsHg7Ym/FIFq6xfh3ZAfrJOvxzF5Rr4V4Z7Dmb28b2FxfKMXUnxNG1keZrAh2Wjk6HfbNBtpV1dkvzGMvmJ9/zdbkOo8WWlYBQDfxrzziFw40LolRRgfmMTfgdRHaMG59rmjnm8wcK6xfjezKd8u56qKiFnEs942VqjadAShHOxaWy9iYQzchEdwJhA4ZtMFEku3dmgo7UM2/4BC+AYt/zdbkOo8WWlYBQDfxrzziwyIwmDgN/ZNtzh70jQzkaTaYEgkQE5PiYAE32MSYNsF+qMsdn0rDKD2q1R3lKd++vUQqWklGcClCcyLX011abhovj6LVnG8FtxJVMjRAbB75VLAtVeXbhxC+I3izxSUMzuh4ZzzwB7q2cVWYX3J2kO/AgBk+mU8y4bqztTrJ3OXQLiAGCtN7i3WK0bxQvklfGNama/UdtLFKTSzfbUYaamzUCcSYhJOY6+mEPjcmUVb5VLAtVeXbhxC+I3izxSUMHl4FCp0ZPaexh7yhqBgFue/AgBk+mU8y4bqztTrJ3OVs7hteGj7WLotjXLKdUZzfegqkWClNkprMu9rj1roWxxyjV18NSux0Xw6ECMOxSsy45x71oaO9QALGC62eK9QXusulKu0bpGbgzNcHy6Sf7H1qVgNDJMmglqTUl+4PmsX08KUfjG9AalPqJgtO/5AQzLehZlyfZZxUzr1RpxfAQGTlznF67CrsEcnQNc0mK+2XoRgF+r+kL1mbnJpuAegIhTjjzePSzs+5Ag1WGFKYBAYT7NRkqqZrXfYionDHEU5qvyI7ggRqNVWZVzS5Biy+Ekg6OK7R0jLNMIJu4192+MA2QT+59qlkbaZdJYTRTKMwaTPJO3fOQ02z3MrlTJHSP//IfYBoRzLVmXJsfgp/Yy83J4+nXF0M9MIkzTF/yR075mVWoJWa76i+SZbgRpqq197pyLtfnEmuzc2Q9qAVnUeyBNh/kHZPbMAodx7ubeXJpwWtT7yfQYRDq6nEZwI0xhTyTnci4Qa2Fu89O3K5ZPTqF5cD/32SVal+DSIyR2g=");
            streamWriter.Close();
        }
        SaveData.Data Load = (SaveData.Data)IOHelper.GetData(Application.persistentDataPath+ @"\Save\"+ LoadSelectedData + ".sav", typeof(SaveData.Data));
        Debug.Log("讀取了" + LoadSelectedData);
        PlayerState = Load.PlayerState;
        PlayerVector3 = Load.PlayerVector3;
        PlayerQuaternion = Load.PlayerQuaternion;
        SaveRotx = Load.SaveRotx;
        SaveRoty = Load.SaveRoty;

        Instantiate(PlayerPrefab, PlayerVector3, PlayerQuaternion).name = "Pine";
        if (Load.WolfState.Count > 0)
        {
            for (int A = 0; A < Load.WolfState.Count; A++)     //讀取動物數據
            {
                WolfState.Add(Load.WolfState[A]);           //讀取動物狀態
                WolfVector3.Add(Load.WolfVector3[A]);       //讀取動物座標
                WolfQuaternion.Add(Load.WolfQuaternion[A]); //讀取動物旋轉角度
                if (WolfState[A] == 1)                          //如果動物活著(WolfState=1)才生成
                {
                    Instantiate(WolfPrefab, WolfVector3[A], WolfQuaternion[A]).name = "Wolf" + A;
                    Debug.Log("讀取了第" + (A + 1) + "隻狼," + "狀態為" + WolfState[A] + ",座標為" + WolfVector3[A]);
                }
                if (WolfState[A] == 2)                        //如果動物被附身(WolfState=2)生成後把主角掛在狼身上
                {
                    Wolf = Instantiate(WolfPrefab, WolfVector3[A], WolfQuaternion[A]);
                    GameObject.Find("Pine").GetComponent<PossessedSystem>().Target = Wolf.GetComponentsInChildren<Transform>()[3].gameObject.GetComponent<BoxCollider>();
                    GameObject.Find("Pine").GetComponent<PossessedSystem>().EnterPossessed();
                    Debug.Log("讀取了第" + (A + 1) + "隻狼," + "狀態為" + WolfState[A] + ",座標為" + WolfVector3[A]);
                }
                //Debug.Log(D1.WolfVector3[A]);
                //Debug.Log("讀" + WolfVector3[A]);
            }
        }
        Debug.Log("讀取了派恩的位置,座標為" + PlayerVector3+ ",狀態為"+ PlayerState);
        if (Load.EnemyState.Count > 0)
        {
            for (int E = 0; E < Load.EnemyState.Count; E++)   //讀取敵人數據
            {
                EnemyState.Add(Load.EnemyState[E]);           //讀取敵人狀態
                EnemyVector3.Add(Load.EnemyVector3[E]);       //讀取敵人座標
                EnemyQuaternion.Add(Load.EnemyQuaternion[E]); //讀取敵人旋轉角度
                if (EnemyState[E] == 1)                       //如果敵人活著(EnemyState=1)才生成
                {
                    Instantiate(EnemyPrefab, EnemyVector3[E], EnemyQuaternion[E]).name = "Enemy" + E;
                    Debug.Log("讀取了第" + (E + 1) + "個敵人," + "狀態為" + EnemyState[E] + ",座標為" + EnemyVector3[E]);
                }
            }
        }


    }

}
