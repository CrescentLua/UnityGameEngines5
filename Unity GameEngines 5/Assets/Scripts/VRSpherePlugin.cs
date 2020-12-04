using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct MyStruct
{
   public bool createVRSphere;
   [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
   public float[] Position;
}

public class VRSpherePlugin : MonoBehaviour
{
    const string dllFile = "VRSpherePlugin";

    [DllImport(dllFile)]
    private static extern IntPtr HelloWorld();

    [DllImport(dllFile)]
    private static extern IntPtr GetStruct();

    [DllImport(dllFile)]
    private static extern void SetStruct(IntPtr t_, bool createVRSphere_, float posX_, float posY_, float posZ_);


    MyStruct t = (MyStruct)Marshal.PtrToStructure(GetStruct(), typeof(MyStruct));
    public Transform VRSpherePrefab; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Marshal.PtrToStringAnsi(HelloWorld()));
       
        IntPtr tPtr = Marshal.AllocHGlobal(Marshal.SizeOf(t));
        Marshal.StructureToPtr(t, tPtr, true);
        SetStruct(tPtr, true, 0.0f, 0.0f, 0.0f);

        CreateVRSphere();
    }

    void CreateVRSphere()
    {
        if (t.createVRSphere)
        {
            Instantiate(VRSpherePrefab, new Vector3(t.Position[0], t.Position[1], t.Position[2]), Quaternion.identity);
        }

        else
        {
            Debug.LogWarning("createVRSphere Boolean is false!");
        }
    }
}
