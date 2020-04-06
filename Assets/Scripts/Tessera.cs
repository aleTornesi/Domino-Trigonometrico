using System;
using UnityEngine;

public class Tessera
{
    private string arg;
    private string function;
    public bool anchored = false;
    public Tessera()
    {
        switch(UnityEngine.Random.Range(0, 2))
        {
            case 0:
                switch(UnityEngine.Random.Range(0, 3))
                {
                     case 0:
                        this.function = "sin";
                        switch (UnityEngine.Random.Range(0, 16))
                        {
                            case 0:
                                this.arg = "0";
                                break;
                            case 1:
                                this.arg = "30";
                                break;
                            case 2:
                                this.arg = "45";
                                break;
                            case 3:
                                this.arg = "60";
                                break;
                            case 4:
                                this.arg = "90";
                                break;
                            case 5:
                                this.arg = "120";
                                break;
                            case 6:
                                this.arg = "135";
                                break;
                            case 7:
                                this.arg = "150";
                                break;
                            case 8:
                                this.arg = "180";
                                break;
                            case 9:
                                this.arg = "210";
                                break;
                            case 10:
                                this.arg = "225";
                                break;
                            case 11:
                                this.arg = "240";
                                break;
                            case 12:
                                this.arg = "270";
                                break;
                            case 13:
                                this.arg = "300";
                                break;
                            case 14:
                                this.arg = "315";
                                break;
                            case 15:
                                this.arg = "330";
                                break;
                            default:
                                throw new System.Exception("Hai saltato un case MONA!");
                                                  
                        }    
                    break;

                    case 1:
                        this.function = "cos";
                        switch (UnityEngine.Random.Range(0, 16))
                        {
                            case 0:
                                this.arg = "0";
                                break;
                            case 1:
                                this.arg = "30";
                                break;
                            case 2:
                                this.arg = "45";
                                break;
                            case 3:
                                this.arg = "60";
                                break;
                            case 4:
                                this.arg = "90";
                                break;
                            case 5:
                                this.arg = "120";
                                break;
                            case 6:
                                this.arg = "135";
                                break;
                            case 7:
                                this.arg = "150";
                                break;
                            case 8:
                                this.arg = "180";
                                break;
                            case 9:
                                this.arg = "210";
                                break;
                            case 10:
                                this.arg = "225";
                                break;
                            case 11:
                                this.arg = "240";
                                break;
                            case 12:
                                this.arg = "270";
                                break;
                            case 13:
                                this.arg = "300";
                                break;
                            case 14:
                                this.arg = "315";
                                break;
                            case 15:
                                this.arg = "330";
                                break;
                            default:
                                throw new System.Exception("Hai saltato un case MONA!");
                        }
                        break;
                    case 2:
                        this.function = "tan";
                        switch (UnityEngine.Random.Range(0, 13))
                        {
                            case 0:
                                this.arg = "0";
                                break;
                            case 1:
                                this.arg = "30";
                                break;
                            case 2:
                                this.arg = "45";
                                break;
                            case 3:
                                this.arg = "60";
                                break;
                            case 4:
                                this.arg = "120";
                                break;
                            case 5:
                                this.arg = "135";
                                break;
                            case 6:
                                this.arg = "150";
                                break;
                            case 7:
                                this.arg = "210";
                                break;
                            case 8:
                                this.arg = "225";
                                break;
                            case 9:
                                this.arg = "240";
                                break;
                            case 10:
                                this.arg = "300";
                                break;
                            case 11:
                                this.arg = "315";
                                break;
                            case 12:
                                this.arg = "330";
                                break;
                            default:
                                throw new System.Exception("Hai saltato un case MONA!");
                        }
                        break;
                }
                break;
            case 1:
                this.function = null;
                switch (UnityEngine.Random.Range(0, 11))
                {
                    case 0:
                        this.arg = "0";
                        break;
                    case 1:
                        this.arg = "1/2";
                        break;
                    case 2:
                        this.arg = "√2/2";
                        break;
                    case 3:
                        this.arg = "√3/2";
                        break;
                    case 4:
                        this.arg = "1";
                        break;
                    case 5:
                        this.arg = "-1/2";
                        break;
                    case 6:
                        this.arg = "-√2/2";
                        break;
                    case 7:
                        this.arg = "-√3/2";
                        break;
                    case 8:
                        this.arg = "-1";
                        break;
                    case 9:
                        this.arg = "√3/3";
                        break;
                    case 10:
                        this.arg = "√3";
                        break;
                }
                break;
        }
    }

    public string getArg()
    {
        return this.arg;
    }

    public string getFunction()
    {
        return this.function;
    }

    public override string ToString()
    {
        if (this.function != null)
            return $"{this.function}({this.arg})";
        return $"{this.arg}";
    }

    public float value() 
    {
        try
        {
            switch (this.function)
            {
                case "sin":
                    return Mathf.Sin(toRadians(float.Parse(this.arg)));
                case "cos":
                    return Mathf.Cos(toRadians(float.Parse(this.arg)));
                case "tan":
                    return Mathf.Tan(toRadians(float.Parse(this.arg)));
                default:
                    string[] members = this.arg.Split('/');
                    if (members.Length == 2)
                    {
                        if (members[0].Contains("√"))
                            if (members[0].Contains("-"))
                            {
                                return Mathf.Sqrt(float.Parse(members[0].Remove(1, 2))) / float.Parse(members[1]);
                            }
                            else
                            {
                                return float.Parse(members[0].Remove(0, 1)) / float.Parse(members[1]);

                            }
                        return float.Parse(members[0]) / float.Parse(members[1]);
                    }
                    else if (members.Length == 1)
                    {
                        if (members[0].Contains("√"))
                            return Mathf.Sqrt(float.Parse(members[0].Remove(0, 1)));
                        return float.Parse(members[0]); 
                    }
                    else
                        throw new ArgumentException("Invalid argument");
            }
        }
        catch (FormatException)
        {
            string[] members = this.arg.Split('/');
            if (members.Length == 2)
            {
                if (members[0].Contains("√"))
                    if (members[0].Contains("-"))
                    {
                        Debug.Log($"{members[0].Remove(1, 2)}/{float.Parse(members[1])}");
                    }
                    else
                    {
                        Debug.Log($"{members[0].Remove(0, 1)} / {members[1]}");

                    }
                Debug.Log($"{members[0]} / {members[1]}");
            }
            else if (members.Length == 1)
            {
                if (members[0].Contains("√"))
                    Debug.Log(Mathf.Sqrt(float.Parse(members[0].Remove(0, 1))));
                Debug.Log($"{members[0]}");
            }
            return 0;
        }
    }
    float toRadians(float degrees)
    {
            return degrees * (Mathf.PI/180);
    }
}
