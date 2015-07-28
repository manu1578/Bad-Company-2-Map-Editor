﻿using UnityEngine;using System.Collections;using System.Xml.Serialization;using System.Xml;		public class MapReader : MonoBehaviour {	public XmlDocument mapXML;	public TextAsset map;	public GameObject placeholder;	void Start()	{		mapXML = new XmlDocument ();		mapXML.LoadXml (map.text);			//Point to the book nodes and process them		ProcessMap(mapXML.SelectNodes("partition/instance"));			}		//Converts an XmlNodeList into Book objects and shows a book out of it on the screen	private void ProcessMap(XmlNodeList nodes)	{		Instance instance;		foreach (XmlNode node in nodes)		{			instance = new Instance();			if(node.Attributes.GetNamedItem("type").Value == "Entity.ReferenceObjectData") 			{ 				string coordinates = node.SelectSingleNode("complex").InnerText;				instance.transformation = coordinates;			}			LoadInstance(instance);		}	}		private void LoadInstance(Instance instance)	{		Vector3 pos = new Vector3();		pos = Vector3.zero;		if (instance.transformation != null) {			string coordiantes = instance.transformation;			string[] coords = coordiantes.Split ('/');			int numcoords = coords.Length;			float x = float.Parse (coords [(numcoords - 4)]);			float y = float.Parse (coords [(numcoords - 3)]);			float z = float.Parse (coords [(numcoords - 2)]);			pos = new Vector3(x,y,z);		}				GameObject ins = GameObject.Instantiate(placeholder.gameObject, pos, Quaternion.identity) as GameObject;		ins.name = instance.transformation;	}	}