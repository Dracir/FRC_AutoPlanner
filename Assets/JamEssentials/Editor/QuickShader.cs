using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[System.Serializable]
public class QuickShader : EditorWindowBase{

public static readonly string PROPERTIES_HELPER_LEFT ="Color\nInteger\nFloat\nVector\nTexture\n";
public static readonly string PROPERTIES_HELPER_RIGHT =
@": c *name* *default*
: i *name* *default*
: f *name* {min,max} *default*
: v *name* *default*
: 2d *name*";
	
	public static string shadersFolder;
	[SerializeField]
	public static string shaderName;
	public static bool replace = true;

	//Properties
	public static string properties = "2d Palette\nf PaletteIndex 0,1 1";
	public static bool texcoord = true;
	public static bool scrPos = false;
	public static bool mainText = false;

	private static readonly string TABS = "\t\t\t";
	private static readonly string TAB_LESS = "\t\t";


	[MenuItem ("Window/JamEssentials/Quick Shader")]
    public static void ShowWindow () {
        EditorWindow.GetWindow(typeof(QuickShader));
		if(shadersFolder == null || true){
			shadersFolder= Application.dataPath;
		}
    }

	void OnGUI()
	{
		DoHeader("Base Settings");
		shadersFolder = DoOpenFolderChooser("Shader Folder",shadersFolder);
		EditorGUILayout.BeginHorizontal();
		shaderName = DoTextBox("Name",shaderName);
		replace = DoCheckBox("Replace", replace,GUILayout.Width(200));
		EditorGUILayout.EndHorizontal();

		DoHorizontalLine();

		EditorGUILayout.LabelField("Properties :");
		EditorGUILayout.BeginHorizontal();
		properties = EditorGUILayout.TextArea(properties);
		DoTightLabel(PROPERTIES_HELPER_LEFT);
		DoTightLabel(PROPERTIES_HELPER_RIGHT);
		EditorGUILayout.EndHorizontal();

		
		scrPos = DoCheckBox("Screen Position", scrPos);
		mainText = DoCheckBox("Main texture", mainText);
		if(!mainText) texcoord = false;
		texcoord = DoCheckBox("Texture Coord", texcoord);
		if(texcoord) mainText = true;


		DoHorizontalLine();
		

		if(DoButton("Generate")){
			Generate();
		}
	}

	private void Generate()
	{
		string text = GenerateShaderText();


		var path = Application.dataPath + "/Shaders/Test.shader";
		if(File.Exists(path)){
			if(!replace){
				Debug.LogError("File already existe");
				return;
			}
		}else{
			//var fs = 
			File.Create(path);
		}
		
		File.WriteAllText(path,text);
	}


	string propertiesShaderLab = "";
	string propertiesShader = "";
	string appdata = "";
	string v2f = "";
	string vert = "";
	string frag = "";

	void AddToShaderLab(string value){
		propertiesShaderLab += value + "\n" + TAB_LESS;
	}
	void AddToShader(string value){
		propertiesShader += value + ";\n" + TAB_LESS;
	}
	void AddToAppData(string value){
		appdata += value + ";\n" + TABS;
	}
	void AddToV2F(string value){
		v2f += value + ";\n" + TABS;
	}
	void AddToVert(string value){
		vert += value + ";\n" + TABS;
	}
	void AddToFrag(string value){
		frag += value + ";\n" + TABS;
	}

	private string GenerateShaderText(){
		string newShader = baseShader.Replace("{0}",shaderName);

		propertiesShaderLab = "";
		propertiesShader = "";
		appdata = "";
		v2f = "";
		vert = "";
		frag = "";
	
		foreach (var p in properties.Split('\n'))
		{
			var args = p.Split(' ');
			if(args[0].ToLower() == "c"){
				AddToShaderLab( string.Format("{0}(\"{0}\", Color) = {1}",args[1],args[2]));
				AddToShader( string.Format("fixed4 {0}",args[1]));
			} else if(args[0].ToLower() == "f"){
				if(args.Length == 4)//Range
					AddToShaderLab( string.Format("{0}(\"{0}\", Range({1})) = {2}",args[1],args[2],args[3]));	
				else//normal float
					AddToShaderLab( string.Format("{0}(\"{0}\", Float) = {1}",args[1],args[2]) );
				
				AddToShader( string.Format("float {0}",args[1]));
				
			} else if(args[0].ToLower() == "i"){
				AddToShaderLab( string.Format("{0}(\"{0}\", Int) = {1}",args[1],args[2]));
				AddToShader( string.Format("int {0}",args[1]));
			} else if(args[0].ToLower() == "v"){
				AddToShaderLab( string.Format("{0}(\"{0}\", Vector) = {1}",args[1],args[2]));
				AddToShader( string.Format("fixed4 {0}",args[1]));
			} else if(args[0].ToLower() == "2d"){
				AddToShaderLab( string.Format("{0}(\"{0}\", 2D) = \"white\" {{}}",args[1]));
				AddToShader( string.Format("sampler2D {0}",args[1]));
				AddToShader( string.Format("float4 {0}_ST", args[1]) );
				AddToShader( string.Format("float4 {0}_TexelSize", args[1]) );
				AddToFrag( string.Format("fixed4 col{0} = tex2D({0}, float2(1,1))", args[1]));
			}
		}

		if(texcoord){
			AddToAppData( "float2 texcoord : TEXCOORD0" );
			AddToV2F( "float2 texcoord : TEXCOORD0");
			AddToVert( "o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex)");
			AddToFrag( "//o.texcoord");
		}
		if(scrPos){
			AddToV2F( "float4 scrPos : TEXCOORD1");
			AddToVert( "o.scrPos = ComputeScreenPos(o.vertex)");
			AddToFrag( "//i.scrPos");
		}
		if(mainText){
			AddToShaderLab( "_MainTex(\"Base (RGBA)\", 2D) = \"white\" {}");
			AddToShader( "sampler2D _MainTex" );
			AddToShader( "float4 _MainTex_ST" );
			AddToShader( "float4 _MainTex_TexelSize" );
			if(texcoord)
				AddToFrag( "fixed4 col = tex2D(_MainTex, i.texcoord)");
				else
				AddToFrag( "fixed4 col = tex2D(_MainTex, float2(1,1))");
		}
		
		propertiesShaderLab = fix(propertiesShaderLab);
		propertiesShader = fix(propertiesShader);
		appdata = fix(appdata);
		v2f = fix(v2f);
		vert = fix(vert);
		frag = fix(frag);

		newShader = newShader.Replace("{1}",propertiesShaderLab);
		newShader = newShader.Replace("{2}",propertiesShader);
		newShader = newShader.Replace("{appdata_t}",appdata);
		newShader = newShader.Replace("{v2f}",v2f);
		newShader = newShader.Replace("{vert}",vert);
		newShader = newShader.Replace("{frag}",frag);

		return newShader;
	}

	private string fix(string item){
		if(item.Length == 0)
			return item;
		else
			return item.Remove(item.LastIndexOf('\n'));
	}

	

	private static string baseShader = 
@"
Shader ""{0}"" {
	Properties{
		{1}
	}

	SubShader{
	Tags{ ""RenderType"" = ""Opaque"" }
	LOD 100

	Pass{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 2.0
		#pragma multi_compile_fog

		#include ""UnityCG.cginc""

		struct appdata_t {
			float4 vertex : POSITION;
			{appdata_t}
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			{v2f}
		};

		{2}


		//----------------------------------------------

		v2f vert(appdata_t v){
			v2f o;

			o.vertex = UnityObjectToClipPos(v.vertex);
			{vert}

			return o;
		}

		fixed4 frag(v2f i) : COLOR{
			{frag}

			return fixed4(1,1,0,1);
		}



		ENDCG
	}
	}
}
";
}
