using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class XmlReader {

    private string[] choiceAttributes;
    public string[] ChoiceAttributes
    {
        get {return choiceAttributes;}
    }

    public string ReadXml(string fileName, string path, string node, int id)
    {
        /*
        WHEN LOADING FROM RESOURCES, DO NOT INCLUDE FILE EXTENSION IN NAME
        */

        XmlNodeList nodes = PrepareXML(fileName, path);

        return nodes[id].SelectSingleNode(node).FirstChild.Value;
    }

    public string[] ReadSubnodes(string fileName, string path, int id)
    {

        XmlNodeList nodes = PrepareXML(fileName, path);
        
        XmlNodeList subnodes = nodes[id].SelectNodes("Response");//find any subnodes with the response tag

        string[] choiceDescs = new string[subnodes.Count];

        choiceAttributes = new string[subnodes.Count];

        for (int i = 0; i < subnodes.Count; i++)
        {
            //Debug.Log(subnodes[i].FirstChild.Value + " " + subnodes[i].SelectSingleNode("@next").Value);

            choiceAttributes[i] = subnodes[i].SelectSingleNode("@next").Value;

            choiceDescs[i] = subnodes[i].FirstChild.Value;
        }

        return choiceDescs;
    }


    XmlNodeList PrepareXML(string fileName, string path)
    {
        var textFile = Resources.Load(fileName) as TextAsset;

        //load xml into memory
        XmlDocument xmlDoc = new XmlDocument();
        string xml = textFile.text;
        xmlDoc.LoadXml(xml);

        //start parsing xml

        XmlNodeList nodes = xmlDoc.SelectNodes(path);

        return nodes;
    }
}
