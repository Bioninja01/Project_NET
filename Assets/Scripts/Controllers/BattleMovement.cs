using UnityEngine;
using System.Collections;

public class BattleMovement : CharaterBattle {

    public GameObject projectile;

    public float speed = 2.0f;
    public float mag = 2.0f;

    Vector3 pos;
    Transform tr;

    AudioClip shoot;
    BattleChip battleChip;

    public int[,] grid;
    BattleGrid bg;
    bool canWalk = false;
    bool finishedWalking = true;
    GridPostion gridPosition = new GridPostion(0, 1);



    // Use this for initialization
    void Start() {
        pos = transform.position;
        tr = transform;
        music = GetComponent<AudioSource>();
        bg = GameObject.FindGameObjectWithTag("BattleArea").GetComponent<BattleGrid>();
        shoot = Resources.Load("Music/shoot1") as AudioClip;
        damage = Resources.Load("Music/Hit1") as AudioClip;
        battleChip = GetComponent<BattleChip>();
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject bullet = Instantiate(projectile, (pos + tr.forward * (mag - .5F)), Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2250);
            music.PlayOneShot(shoot, .5f);
        }
        if (Input.GetKeyDown(KeyCode.F)) { battleChip.Atk(this); }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { tr.eulerAngles = new Vector3(0, 0, 0); }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { tr.eulerAngles = new Vector3(0, 180, 0); }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { tr.eulerAngles = new Vector3(0, -90, 0); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { tr.eulerAngles = new Vector3(0, 90, 0); }

        if (finishedWalking) {
           if (Input.GetKey(KeyCode.D) && tr.position == pos) {
                canWalk = (Physics.CheckSphere(pos + ((Vector3.right + Vector3.down / 2) * mag), 1f));
                if (canWalk) {
                    gridPosition.x++;
                    pos += Vector3.right * mag;
                    finishedWalking = false;
                }
            }
            else if (Input.GetKey(KeyCode.A) && tr.position == pos) {
                canWalk = (Physics.CheckSphere(pos + ((Vector3.left + Vector3.down / 2) * mag), 1f));
                if (canWalk) {
                    gridPosition.x--;
                    pos += Vector3.left * mag;
                    finishedWalking = false;
                }
            }
            else if (Input.GetKey(KeyCode.W) && tr.position == pos) {
                canWalk = (Physics.CheckSphere(pos + ((Vector3.forward + Vector3.down / 2) * mag), 1f));
                if (canWalk) {
                    gridPosition.y--;
                    pos += Vector3.forward * mag;
                    finishedWalking = false;
                }

            }
            else if (Input.GetKey(KeyCode.S) && tr.position == pos) {
                canWalk = (Physics.CheckSphere(pos + ((Vector3.back + Vector3.down / 2) * mag), 1f));
                if (canWalk) {
                    gridPosition.y++;
                    pos += Vector3.back * mag;
                    finishedWalking = false;
                }
            }
        }
        if(pos != transform.position) {
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
            if (transform.position == pos) {
                InvokeRepeating("test", .1f, .3f);
            }
        }
        
    }
    void test() {
        finishedWalking = true;

        CancelInvoke();
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
