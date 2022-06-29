using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100; //기본 체력 100으로 설정
    public int currentHealth; // 현재 체력 세팅
    public Slider healthSlider; // 체력 게이지 UI 와 연결된 변수
    public Image damageImage; //데미지를 입었을시 화면의 변화를 주기 위한 이미지.
    public AudioClip deathCilp; // 데미지를 입었을시 소리 출력

    Animator anim; // 애니메이터 컨트롤러에 매개변수를 전달하기 위한 Animator 컴포넌트

    AudioSource playerAudio; // 플레이어 게임 오브젝트에 붙어있는 오디오 소스 컴포넌트, 오디오 재생에 필요

    PlayerMovement playerMovement; // 플레이어의 움직임을 관리하는 PlayerMovement 스크립트 컴포넌트

    public float flshSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 100f / 250f);

    bool isDead; // 플레이어가 죽었는지 저장하는 변수
    bool damaged; // 플레이어가 피해를 입었는지 저장하는 변수

    void Awake()
    {
        anim = GetComponent<Animator>(); // Player게임 오브젝트에 붙어있는 Animator컴포넌트를 찾아 변수에 넣음

        playerAudio = GetComponent<AudioSource>(); // Player 게임 오브젝트에 붙어 있는 AudioSource 컴포넌트를 찾아 변수에 넣음

        playerMovement = GetComponent<PlayerMovement>(); // player 게임 오브젝트에 붙어있는 PlayerMovement 컴포넌트를 찾아 변수에 넣음.

        currentHealth = startHealth; // 시작시 현재체력을 최대체력으로 만듦

    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;//데미지를 받자마자 빨간색으로 변경.
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flshSpeed * Time.deltaTime); // 공격 받은 후에는 서서히 투명한 색으로 변경.
        }
        damaged = false;
    }

    // 플레이어가 공격을 받았을때 호출함수
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount; //데미지를 입었을시 현재 체력에서 데미지 값을 차감

        healthSlider.value = currentHealth; // 체력게이지의 값을 현재체력과 동일하게 함.
        
        if( currentHealth <= 0 && !isDead) // 만약 현재 체력이 0이하 라면
        {
            //Death(); // 죽었다는 함수 호출
        }
        else // 죽은게 아니면 Damage Trigger 발동
        {
            anim.SetTrigger("Damage");
        }
    }

    void Death()
    {

        StageController.instance.FinishGame();

        isDead = true; // 캐릭터가 죽었다면 isDead 플래그를 True로 설정.

        anim.SetTrigger("Die"); // 애니메이션에서 Die라는 트리거 발동.

        playerMovement.enabled = false; // 캐릭터의 움직임을 관리하난 PlayerMovement 스크립트를 비활성화.


    }
}
