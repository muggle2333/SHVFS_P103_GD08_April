// Fill out your copyright notice in the Description page of Project Settings.


#include "TP_ThirdPersonCharacter.h"
#include "Camera/CameraComponent.h"
#include "GameFramework/SpringArmComponent.h"
#include "Containers/UnrealString.h"

// Sets default values
ATP_ThirdPersonCharacter::ATP_ThirdPersonCharacter()
{
 	// Set this character to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
	BaseTurnRate = 45.f;
	BaseLoopUpRate = 45.F;

	CameraBoom = CreateDefaultSubobject<USpringArmComponent>(TEXT("CameraBoom"));
	CameraBoom->SetupAttachment(RootComponent);
	CameraBoom->TargetArmLength = 300.0f;
	CameraBoom->bUsePawnControlRotation = true;

	FollowCamera = CreateDefaultSubobject<UCameraComponent>(TEXT("FollowCamera"));
	FollowCamera->SetupAttachment(CameraBoom,USpringArmComponent::SocketName);
	FollowCamera->bUsePawnControlRotation = false;

	MoveMaxSpeed = 100.f;
	//Velocity = FVector(0, 0, 0);
	Velocity = FVector::ZeroVector;
}


// Called when the game starts or when spawned
void ATP_ThirdPersonCharacter::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void ATP_ThirdPersonCharacter::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	if (Velocity.X != 0 || Velocity.Y != 0)
	{
		GEngine->AddOnScreenDebugMessage(-1, 10.0f, FColor::Yellow, FString::Printf(TEXT("x:%d,y:%d"), Velocity.X, Velocity.Y));
	}
	
	AddActorLocalOffset(Velocity * DeltaTime);
}

// Called to bind functionality to input
void ATP_ThirdPersonCharacter::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);
	check(PlayerInputComponent);
	PlayerInputComponent->BindAxis("MoveForward", this, &ATP_ThirdPersonCharacter::MoveForward);
	PlayerInputComponent->BindAxis("MoveRight", this, &ATP_ThirdPersonCharacter::MoveRight);
	
	PlayerInputComponent->BindAxis("Turn", this, &APawn::AddControllerYawInput);
	//PlayerInputComponent->BindAxis("TurnRate", this, &ATP_ThirdPersonCharacter::TurnAtRate);
	PlayerInputComponent->BindAxis("LoopUp", this, &APawn::AddControllerYawInput);
	//PlayerInputComponent->BindAxis("LookUpRate", this, &ATP_ThirdPersonCharacter::LookUpAtRate);

}

void ATP_ThirdPersonCharacter::MoveForward(float Value)
{
	Velocity.X = FMath::Clamp(Value,-1.0f,1.0f)*MoveMaxSpeed;
}

void ATP_ThirdPersonCharacter::MoveRight(float Value)
{
	Velocity.Y = FMath::Clamp(Value, -1.0f, 1.0f) * MoveMaxSpeed;
}

