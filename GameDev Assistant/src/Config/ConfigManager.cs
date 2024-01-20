﻿using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;

namespace GameDevAssistant.Config
{
    [BepInPlugin(GameDevAssistant.PluginGuid, GameDevAssistant.PluginName, GameDevAssistant.PluginVersion)]
    [BepInProcess("Mad Games Tycoon 2.exe")]
    public class ConfigManager
    {
        private ConfigFile ConfigFile { get; set; }

        //For Unity Default Color List
        private enum ColorOptions
        {
            Black,
            Blue,
            Brown,
            Cyan,
            DarkBlue,
            Green,
            Grey,
            LightBlue,
            Magenta,
            Maroon,
            Navy,
            Olive,
            Orange,
            Purple,
            Red,
            Silver,
            Teal,
            White,
            Yellow
        }

        /// <summary>
        /// Constructor with LoadConfig
        /// </summary>
        /// <param name="configFile"></param>
        public ConfigManager(ConfigFile configFile)
        {
            ConfigFile = configFile;
            LoadConfig();
        }

        // =============================================================================================================
        // Config sections
        // =============================================================================================================
        private const string ModSettingsSection = "0. MOD Settings";
        private const string MainSettingSection = "1. General Setting";
        private const string ColorSettingSection = "2. Coloring Settings";

        // =============================================================================================================
        // Config entries 
        // =============================================================================================================
        public static ConfigEntry<bool> IsModEnabled { get; private set; }

        // Theme Config ---------------------------------------------------------------
        public static ConfigEntry<bool> IsAssistThemeEnabled { get; private set; }

        // Genres Config ---------------------------------------------------------------
        public static ConfigEntry<bool> IsAssistGenreEnabled { get; private set; }

        // AgeTarget Config ---------------------------------------------------------------
        public static ConfigEntry<bool> IsAssistAgeTargetEnabled { get; private set; }

        // Slider Config ---------------------------------------------------------------

        public static ConfigEntry<bool> IsAssistAutoDesignSliderEnabled { get; private set; }

        // Name Config ---------------------------------------------------------------

        public static ConfigEntry<bool> IsAssistRandomNameEnabled { get; private set; }

        // -----------------------------------------------------------------------------

        private static ConfigEntry<ColorOptions> ListColorGood { get; set; }
        private static ConfigEntry<ColorOptions> ListColorNormal { get; set; }
        private static ConfigEntry<ColorOptions> ListColorGoodSelected { get; set; }
        private static ConfigEntry<ColorOptions> ListColorNormalSelected { get; set; }
        public static Color ColorGood { get; private set; }
        public static Color ColorNormal { get; private set; }
        public static Color ColorGoodSelected { get; private set; }
        public static Color ColorNormalSelected { get; private set; }
        
        /// <summary>
        /// Loading when the game starts
        /// </summary>
        private void LoadConfig()
        {
            // =============================================================================================================
            // Config setting definitions here
            // =============================================================================================================
            
            // Main Settings
            IsModEnabled = ConfigFile.Bind(
                ModSettingsSection,
                "Activate the MOD",
                true,
                "Toggle 'Enabled' to activate the mod");

            // ----------------------------------------------------------------------------------------------------------------
            // Theme Config
            IsAssistThemeEnabled = ConfigFile.Bind(
                MainSettingSection,
                "Assist Themes",
                true,
                "Enabling this option assists with the Theme selection in the theme menu.");

            // ----------------------------------------------------------------------------------------------------------------
            // Genre Config
            IsAssistGenreEnabled = ConfigFile.Bind(
                MainSettingSection,
                "Assist Genres",
                true,
                "Enabling this option assists with the Genre selection in the genre menu.");
            // ----------------------------------------------------------------------------------------------------------------

            // AgeTarget Config
            IsAssistAgeTargetEnabled = ConfigFile.Bind(
                MainSettingSection,
                "Assist Age Target Group",
                true,
                "Enabling this option assists with the Age Target Group selection in the age target menu.");
            // ----------------------------------------------------------------------------------------------------------------

            // Auto Design Slider Config
            IsAssistAutoDesignSliderEnabled = ConfigFile.Bind(
                MainSettingSection,
                "Assist Auto Design Slider",
                true,
                "Enabling this option assists with adjusting the Concept Slider in the concept slider menu.");

            // Random Name Config
            IsAssistRandomNameEnabled = ConfigFile.Bind(
                MainSettingSection,
                "Assist Random Name",
                true,
                "Enabling this option assists with the Random Name with the assist button.");

            // ----------------------------------------------------------------------------------------------------------------

            // Color Settings
            ListColorGood = ConfigFile.Bind(
                ColorSettingSection,
                "Fit Color",
                ColorOptions.Green,
                new ConfigDescription("Color indicating a good fit with the genre"));

            ListColorNormal = ConfigFile.Bind(
                ColorSettingSection,
                "Normal Color",
                ColorOptions.White,
                new ConfigDescription("Color indicating a normal or neutral fit with the genre"));

            ListColorGoodSelected = ConfigFile.Bind(
                ColorSettingSection,
                "Fit Color When Selected",
                ColorOptions.Olive,
                new ConfigDescription("Color indicating a good fit with the genre when selected"));

            ListColorNormalSelected = ConfigFile.Bind(
                ColorSettingSection,
                "Normal Color When Selected",
                ColorOptions.Grey,
                new ConfigDescription("Color indicating a normal or neutral fit with the genre when selected"));

            // =============================================================================================================
            SetColorSetting();
            // Config setting event handlers here
            ConfigFile.SettingChanged += OnConfigSettingChanged;
            // =============================================================================================================
        }

        /// <summary>
        /// DEBUG: Event handler for config setting changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConfigSettingChanged(object sender, SettingChangedEventArgs e)
        {
#if DEBUG
            Debug.Log(GameDevAssistant.PluginName + " : Config setting is changed");
#endif
            SetColorSetting();
        }

        private void SetColorSetting()
        {
            ColorGood = Helper.GetColor(ListColorGood.Value.ToString());
            ColorNormal = Helper.GetColor(ListColorNormal.Value.ToString());
            ColorGoodSelected = Helper.GetColor(ListColorGoodSelected.Value.ToString());
            ColorNormalSelected = Helper.GetColor(ListColorNormalSelected.Value.ToString());
        }
    }
}