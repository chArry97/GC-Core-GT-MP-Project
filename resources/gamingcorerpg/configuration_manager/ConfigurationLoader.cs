using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class ConfigurationLoader {
	
	private static ConfigurationLoader conLoader;
	
	private readonly Dictionary<string, XmlDocument> _xmlConfigs = new Dictionary<string, XmlDocument>();

	private ConfigurationLoader() {
		string[] fileEntries = Directory.GetFiles(TTRPG.Api.getResourceFolder() + "/config");
		foreach (string fileEntry in fileEntries) {
			string configName = Path.GetFileNameWithoutExtension(fileEntry);
			XmlDocument config = new XmlDocument();
			config.Load(fileEntry);

			_xmlConfigs.Add(configName, config);
		}
	}
	
	public static ConfigurationLoader getConfigurationLoader() {
		if (conLoader == null) {
			conLoader = new ConfigurationLoader();
		}
		return conLoader;
	}

	public XmlDocument GetConfig(string configName) {
		return _xmlConfigs.Get(configName);
	}

	public Boolean ConfigExists(string configName) {
		return _xmlConfigs.ContainsKey(configName);
	}
}