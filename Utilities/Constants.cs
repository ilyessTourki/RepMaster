using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrainSheet.Model;
using TrainSheet.Model.Enum;
using TrainSheet.Model.ServiceModel;

namespace TrainSheet.Utilities;

public static class Constants
{
    #region Pec
    public static MuscleCategory assistedBarChest = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec ,
        name = "Assisted Bar Chest",
        image = "assistedbarchest.png",
        bestRepetition = new Repetition{weight=1,repetion=1},
        bestWeight = 2,
        lastRepetition = new List<List<Repetition>>
        {
            new List<Repetition>{
                new Repetition{weight=2,repetion=2},
                new Repetition{weight=3,repetion=3},
                new Repetition{weight=2,repetion=2},
                new Repetition{weight=3,repetion=3},
            },
            new List<Repetition>{
                new Repetition{weight=2,repetion=2},
                new Repetition{weight=3,repetion=3},
            },
            new List<Repetition>{
                new Repetition{weight=2,repetion=2},
            },
        }
    };
    public static MuscleCategory assistedBarUpChest = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec ,
        name = "Assisted Bar Up Chest",
        image = "assistedbarupchest.png",
        bestRepetition = new Repetition{weight=1,repetion=1},
        bestWeight  = 20,
        lastRepetition = new List<List<Repetition>>
        {
            new List<Repetition>{
                new Repetition{weight=2,repetion=2},
                new Repetition{weight=3,repetion=3},
            },
            new List<Repetition>{
                new Repetition{weight=2,repetion=2},
                new Repetition{weight=3,repetion=3},
            },
            new List<Repetition>{
                new Repetition{weight=2,repetion=2},
            },
        }
    };
    public static MuscleCategory MachineDownChest = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec ,
        name = "Machine Down Chest",
        image = "machinedownchest.png",
    };
    public static MuscleCategory MachineFly = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec ,
        name = "MachineFly",
        image = "machinefly.png",
    };
    public static MuscleCategory MachineIncline = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec ,
        name = "Machine Incline",
        image = "machineupchest.png",
    };
    public static MuscleCategory DumbbellPress = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec,
        name = "Dumbbell Press",
        image = "dumbbellpress.png",
    };
    public static MuscleCategory InclineDumbbellFly = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec,
        name = "Incline Dumbbell Fly",
        image = "inclinedumbbellfly.png",
    };
    public static MuscleCategory InclineDumbbellPress = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec,
        name = "Incline Dumbbell Press",
        image = "inclinedumbbellpress.png",
    };
    public static MuscleCategory Dips = new MuscleCategory
    {
        muscleType = MuscleEnum.Pec,
        name = "Dips",
        image = "dips.png",
    };

    public static List<MuscleCategory> PecExercices = new List<MuscleCategory>();
    #endregion
    
    #region ForeArm
    public static MuscleCategory BarCurl = new MuscleCategory
    {
        muscleType = MuscleEnum.Frontarms ,
        name = "Bar Curl",
        image = "forearm/barcurl.png",
    };
    public static MuscleCategory ReverseCurl = new MuscleCategory
    {
        muscleType = MuscleEnum.Frontarms ,
        name = "Reverse Curl",
        image = "forearm/reversecurl.png",
    };
    public static List<MuscleCategory> FrontArmsExercices = new List<MuscleCategory>();
    #endregion

    #region Back
    public static MuscleCategory CableRow = new MuscleCategory
    {
        muscleType = MuscleEnum.Back,
        name = "Cable Row",
        image = "cablerow.png",
    };
    public static MuscleCategory FrontPullDown = new MuscleCategory
    {
        muscleType = MuscleEnum.Back,
        name = "Front Pull Down",
        image = "frotpulldown.png",
    };
    public static MuscleCategory GripPullUp = new MuscleCategory
    {
        muscleType = MuscleEnum.Back,
        name = "Grip Pul lUp",
        image = "grippullup.png",
    };
    public static MuscleCategory LatPullDown = new MuscleCategory
    {
        muscleType = MuscleEnum.Back,
        name = "Lat Pull Down",
        image = "latpulldown.png",
    };
    public static MuscleCategory TBarRow = new MuscleCategory
    {
        muscleType = MuscleEnum.Back,
        name = "T Bar Row",
        image = "tbbarrow.png",
    };
    public static MuscleCategory TRow = new MuscleCategory
    {
        muscleType = MuscleEnum.Back,
        name = "T Row",
        image = "trow.png",
    };
    public static List<MuscleCategory> BackExercices = new List<MuscleCategory>();
    #endregion

    #region Biceps
    public static MuscleCategory CaleCurl = new MuscleCategory
    {
        muscleType = MuscleEnum.Bieceps,
        name = "Cale Curl",
        image = "biceps/calecurl.png",
    };
    public static MuscleCategory ConcentrationCurl = new MuscleCategory
    {
        muscleType = MuscleEnum.Bieceps,
        name = "Concentration Curl",
        image = "biceps/concentrationcurl.png",
    };
    public static MuscleCategory DumbellCurl = new MuscleCategory
    {
        muscleType = MuscleEnum.Bieceps,
        name = "Dumbell Curl",
        image = "biceps/dumbellcurl.png",
    };
    public static MuscleCategory PreacherCurl = new MuscleCategory
    {
        muscleType = MuscleEnum.Bieceps,
        name = "Preacher Curl",
        image = "biceps/preachercurl.png",
    };
    public static List<MuscleCategory> BicepsExercices = new List<MuscleCategory>();
    #endregion

    #region Legs
    public static MuscleCategory CalfMachine = new MuscleCategory
    {
        muscleType = MuscleEnum.Legs,
        name = "Calf Machine",
        image = "legs/calfmachine.png",
    };
    public static MuscleCategory LegCurl = new MuscleCategory
    {
        muscleType = MuscleEnum.Legs,
        name = "Curl",
        image = "legs/concentrationcurl.png",
    };
    public static MuscleCategory LegExtension = new MuscleCategory
    {
        muscleType = MuscleEnum.Legs,
        name = "Extension",
        image = "legs/legextension.png",
    };
    public static MuscleCategory LegPress = new MuscleCategory
    {
        muscleType = MuscleEnum.Legs,
        name = "Press",
        image = "legs/legpress.png",
    };
    public static MuscleCategory ReverseLegCurl = new MuscleCategory
    {
        muscleType = MuscleEnum.Legs,
        name = "Reverse Curl",
        image = "legs/reverseLegcurl.png",
    };
    public static MuscleCategory SquatsMachine = new MuscleCategory
    {
        muscleType = MuscleEnum.Legs,
        name = "Squats Machine",
        image = "legs/squatsmachine.png",
    };
    public static List<MuscleCategory> LegsExercices = new List<MuscleCategory>();
    #endregion

    #region Shoulders
    public static MuscleCategory ShoulderDumbellPress = new MuscleCategory
    {
        muscleType = MuscleEnum.Shoulder,
        name = "Dumbell Press",
        image = "shoulder/dumbellpress.png",
    };
    public static MuscleCategory FacePull = new MuscleCategory
    {
        muscleType = MuscleEnum.Shoulder,
        name = "Face Pull",
        image = "shoulder/facepull.png",
    };
    public static MuscleCategory LateralRaise = new MuscleCategory
    {
        muscleType = MuscleEnum.Shoulder,
        name = "Lateral Raise",
        image = "shoulder/lateralraise.png",
    };
    public static MuscleCategory MachineLateralRaise = new MuscleCategory
    {
        muscleType = MuscleEnum.Shoulder,
        name = "Machine Lateral Raise",
        image = "shoulder/machinelateralraise.png",
    };
    public static MuscleCategory ReversecaleFly = new MuscleCategory
    {
        muscleType = MuscleEnum.Shoulder,
        name = "Reversecale Fly",
        image = "shoulder/reversecalefly.png",
    };
    public static List<MuscleCategory> ShouldersExercices = new List<MuscleCategory>();
    #endregion

    #region Triceps
    public static MuscleCategory LyingBarBell = new MuscleCategory
    {
        muscleType = MuscleEnum.Triceps,
        name = "Lying Bar Bell",
        image = "triceps/lyingbarbell.png",
    };
    public static MuscleCategory RopeOverHead = new MuscleCategory
    {
        muscleType = MuscleEnum.Triceps,
        name = "Rope Over Head",
        image = "triceps/ropeoverhead.png",
    };
    public static MuscleCategory RopePushDown = new MuscleCategory
    {
        muscleType = MuscleEnum.Triceps,
        name = "Rope Push Down",
        image = "triceps/ropepushdown.png",
    };
    public static List<MuscleCategory> TricepsExercices = new List<MuscleCategory>();
    #endregion

    // Static constructor to populate the list
    static Constants()
    {
        #region Pec
        PecExercices.Add(assistedBarChest);
        PecExercices.Add(assistedBarUpChest);
        PecExercices.Add(MachineDownChest);
        PecExercices.Add(MachineFly);
        PecExercices.Add(MachineIncline);
        PecExercices.Add(DumbbellPress);
        PecExercices.Add(InclineDumbbellFly);
        PecExercices.Add(InclineDumbbellPress);
        PecExercices.Add(Dips);
        #endregion

        #region ForeArm
        FrontArmsExercices.Add(BarCurl);
        FrontArmsExercices.Add(ReverseCurl);
        #endregion

        #region Back
        BackExercices.Add(CableRow);
        BackExercices.Add(FrontPullDown);
        BackExercices.Add(GripPullUp);
        BackExercices.Add(LatPullDown);
        BackExercices.Add(TBarRow);
        BackExercices.Add(TRow);
        #endregion

        #region Biceps
        BicepsExercices.Add(CaleCurl);
        BicepsExercices.Add(ConcentrationCurl);
        BicepsExercices.Add(DumbellCurl);
        BicepsExercices.Add(PreacherCurl);
        #endregion

        #region Legs
        LegsExercices.Add(CalfMachine);
        LegsExercices.Add(LegCurl);
        LegsExercices.Add(LegExtension);
        LegsExercices.Add(LegPress);
        LegsExercices.Add(ReverseLegCurl);
        LegsExercices.Add(SquatsMachine);
        #endregion

        #region Shoulders
        ShouldersExercices.Add(ShoulderDumbellPress);
        ShouldersExercices.Add(FacePull);
        ShouldersExercices.Add(LateralRaise);
        ShouldersExercices.Add(MachineLateralRaise);
        ShouldersExercices.Add(ReversecaleFly);
        #endregion

        #region Triceps
        TricepsExercices.Add(LyingBarBell);
        TricepsExercices.Add(RopeOverHead);
        TricepsExercices.Add(RopePushDown);
        #endregion
    }
    
}
