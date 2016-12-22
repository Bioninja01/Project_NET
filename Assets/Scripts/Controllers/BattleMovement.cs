using UnityEngine;
using System.Collections;

public class BattleMovement : CharaterBattle {

    public GameObject projectile;

    public float speed = 2.0f;
    public float mag = 2.0f;

    Vector3 pos;
    Transform tr;

    AudioClip shoot;

    public int[,] grid;
    BattleGrid bg;
    bool canWalk = false;
    GridPostion gridPosition = new GridPostion(0, 1);



    // Use this for initialization
    void Start() {
        pos = transform.position;
        tr = transform;
        music = GetComponent<AudioSource>();
        bg = GameObject.FindGameObjectWithTag("BattleArea").GetComponent<BattleGrid>();
        shoot = Resources.Load("Music/shoot1") as AudioClip;
        damage = Resources.Load("Music/Hit1") as AudioClip;
    }
    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject bullet = Instantiate(projectile, (pos+ Vector3.right * (mag-.5F)), Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2250);
            music.PlayOneShot(shoot,.5f);
        }

        if (Input.GetKey(KeyCode.RightArrow) && tr.position == pos) {
            if (!(gridPosition.x >= bg.size[0] - 1)) {
                canWalk = (Physics.CheckSphere(pos + ((Vector3.right + Vector3.down / 2) * mag), 1f));
                if (canWalk) {
                    gridPosition.x++;
                    pos += Vector3.right * mag;
                }
            }
            else if (gridPosition.x > bg.size[0] - 1) {
                return;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && tr.position == pos) {
            if (!(gridPosition.x <= 0)) {
                canWalk = (Physics.CheckSphere(pos + ((Vector3.left + Vector3.down / 2) * mag), 1f));
                if (canWalk) {
                    gridPosition.x--;
                    pos += Vector3.left * mag;
                }
            }
            else if (gridPosition.x < 0) {
                return;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && tr.position == pos) {
            if (!(gridPosition.y <= 0)) {
                canWalk = (Physics.CheckSphere(pos + ((Vector3.forward + Vector3.down / 2) * mag), 1f));
                if (canWalk) {
                    gridPosition.y--;
                    pos += Vector3.forward * mag;
                }
            }
            else if (gridPosition.y < 0) {
                return;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && tr.position == pos) {
            if (!(gridPosition.y >= bg.size[1] / 2 - 1)) {
                canWalk = (Physics.CheckSphere(pos + ((Vector3.back + Vector3.down / 2) * mag), 1f));
                if (canWalk) {
                    gridPosition.y++;
                    pos += Vector3.back * mag;
                }
            }
            else if (gridPosition.y > bg.size[1] / 2 - 1) {
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    void OnDrawGizmos() {
        Gizmos.color =  Color.red;
        Gizmos.DrawCube(pos + ((Vector3.right + Vector3.down/2) * mag ), new Vector3(3,1,3));
        Gizmos.DrawCube(pos + ((Vector3.left + Vector3.down/2) * mag), new Vector3(3, 1, 3));
        Gizmos.DrawCube(pos + ((Vector3.forward + Vector3.down/2) * mag), new Vector3(3, 1, 3));
        Gizmos.DrawCube(pos + ((Vector3.back + Vector3.down/2) * mag), new Vector3(3, 1, 3));
    }
    

    class GridPostion {
        public int x;
        public int y;
        public GridPostion(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

}
