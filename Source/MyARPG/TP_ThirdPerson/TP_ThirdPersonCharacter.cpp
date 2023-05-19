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

	//MoveMaxSpeed = 100.f;
	//Velocity = FVector(0, 0, 0);
	//Velocity = FVector::ZeroVector;
}


// Called when the game starts or when spawned
void ATP_ThirdPersonCharacter::BeginPlay()
{
	Super::BeginPlay();

	UWorld* world = this->GetWorld();
	auto controller = world->GetFirstPlayerController();
	UInputComponent* comp = controller->InputComponent;
	//comp->BindAction("Test",IE_Pressed,this,&ATP_ThirdPersonCharacter::Test);
	//SetupPlayerInputComponent(comp);
	//SetActorLocation(FVector::ZeroVector);
	
}

// Called every frame
void ATP_ThirdPersonCharacter::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	/*if (Velocity.X != 0 || Velocity.Y != 0)
	{
		GEngine->AddOnScreenDebugMessage(-1, 10.0f, FColor::Yellow, FString::Printf(TEXT("x:%d,y:%d"), Velocity.X, Velocity.Y));
	}
	
	AddActorLocalOffset(Velocity * DeltaTime);*/
}

// Called to bind functionality to input
void ATP_ThirdPersonCharacter::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);
	check(PlayerInputComponent);
	PlayerInputComponent->BindAxis("MoveForward", this, &ATP_ThirdPersonCharacter::MoveForward);
	PlayerInputComponent->BindAxis("MoveRight", this, &ATP_ThirdPersonCharacter::MoveRight);
	
	//For PC Mouse
	PlayerInputComponent->BindAxis("Turn", this, &APawn::AddControllerYawInput);
	//For Controller
	PlayerInputComponent->BindAxis("TurnRate", this, &ATP_ThirdPersonCharacter::TurnAtRate);
	//For PC Mouse
	PlayerInputComponent->BindAxis("LookUp", this, &APawn::AddControllerPitchInput);
	//For Controller
	PlayerInputComponent->BindAxis("LookUpRate", this, &ATP_ThirdPersonCharacter::LookUpAtRate);

}

void ATP_ThirdPersonCharacter::MoveForward(float Value)
{
	//Velocity.X = FMath::Clamp(Value,-1.0f,1.0f)*MoveMaxSpeed;
	if ((Controller != NULL) && (Value != 0.0f))
	{
		//Find the forward way
		const FRotator Rotation = Controller->GetControlRotation();
		const FRotator YawRotation(0, Rotation.Yaw, 0);
		//get forward vector
		const FVector Direction = FRotationMatrix(YawRotation).GetUnitAxis(EAxis::X);
		AddMovementInput(Direction, Value);
	}
}

void ATP_ThirdPersonCharacter::MoveRight(float Value)
{
	//Velocity.Y = FMath::Clamp(Value, -1.0f, 1.0f) * MoveMaxSpeed;
	if ((Controller != NULL) && (Value != 0.0f))
	{
		//Find the forward way
		const FRotator Rotation = Controller->GetControlRotation();
		const FRotator YawRotation(0, Rotation.Yaw, 0);
		//get forward vector
		const FVector Direction = FRotationMatrix(YawRotation).GetUnitAxis(EAxis::Y);
		AddMovementInput(Direction, Value);
	}
}

void ATP_ThirdPersonCharacter::TurnAtRate(float Rate)
{
	if (Controller != NULL)
	{
		AddControllerYawInput(Rate);
	}
}

void ATP_ThirdPersonCharacter::LookUpAtRate(float Rate)
{
	if (Controller != NULL)
	{
		AddControllerPitchInput(Rate);
	}
}

void ATP_ThirdPersonCharacter::Test()
{
	GEngine->AddOnScreenDebugMessage(-1, 10.0f, FColor::Yellow, FString::Printf(TEXT("Test")));
}
