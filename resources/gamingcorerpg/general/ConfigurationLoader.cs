using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using GrandTheftMultiplayer.Shared;

/// <summary>
/// A ConfigurationLoader is used to load all Config Files that are save in the /config
/// folder inside the gamingcorerpg resource.
/// </summary>
public class ConfigurationLoader {
	
	private static ConfigurationLoader conLoader;
	
	private readonly Dictionary<string, XmlDocument> _xmlConfigs = new Dictionary<string, XmlDocument>();

    private ConfigurationLoader() {
        string[] fileEntries = Directory.GetFiles(Directory.GetCurrentDirectory() + "/resources/gamingcorerpg/config");
		foreach (string fileEntry in fileEntries) {
			string configName = Path.GetFileNameWithoutExtension(fileEntry);
			XmlDocument config = new XmlDocument();
			config.Load(fileEntry);

			_xmlConfigs.Add(configName, config);
		}
	}
	
    /// <summary>
    /// Returns an instance of the ConfigurationLoader. This method should be used so that loading all
    /// config files only needs to be done once.
    /// </summary>
    /// <returns>Single Instance of a ConfigurationLoader object</returns>
	public static ConfigurationLoader getConfigurationLoader() {
		if (conLoader == null) {
			conLoader = new ConfigurationLoader();
		}
		return conLoader;
	}

    /// <summary>
    /// Returns a XmlDocument reprasentation of a config file with the given name.
    /// </summary>
    /// <param name="configName">Name of the config file</param>
    /// <returns>XmlDocument reprasentation of the config file</returns>
	public XmlDocument GetConfig(string configName) {
		return _xmlConfigs.Get(configName);
	}

    /// <summary>
    /// Returns true, when a config with the given name was loaded.
    /// </summary>
    /// <param name="configName">Name of the config file</param>
    /// <returns>true, if a config with the given name exists</returns>
	public Boolean ConfigExists(string configName) {
		return _xmlConfigs.ContainsKey(configName);
	}
}