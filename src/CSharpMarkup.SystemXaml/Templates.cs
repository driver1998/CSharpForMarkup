using System;
using System.Collections.Generic;
using System.Security;
using CSharpMarkup.SystemXaml.Delegators;
using Xaml = Windows.UI.Xaml;
using Controls = Windows.UI.Xaml.Controls;

namespace CSharpMarkup.SystemXaml
{
    public static partial class Helpers
    {
        public static DataTemplate DataTemplate(Func<UIElement> build) => DataTemplate<Xaml.Controls.Grid>(build);

        public static DataTemplate DataTemplate(Action<Xaml.DependencyObject> build) => DataTemplate<Xaml.Controls.Grid>(build);

        public static DataTemplate DataTemplate<TRootUI>(Func<UIElement> build) where TRootUI : Xaml.Controls.Panel, new()
        {
            var ui = (Xaml.DataTemplate)CreateTemplate(nameof(Xaml.DataTemplate), typeof(TRootUI), false, BuildChild.CreateIdFor(build));
            return CSharpMarkup.SystemXaml.DataTemplate.StartChain(ui);
        }

        public static DataTemplate DataTemplate<TRootUI>(Action<Xaml.DependencyObject> build) where TRootUI : Xaml.UIElement, new()
        {
            // Note that we cannot pass markup objects to the build action here, because we get the ui type instance.
            var ui = (Xaml.DataTemplate)CreateTemplate(nameof(Xaml.DataTemplate), typeof(TRootUI), true, ConfigureRoot.CreateIdFor(build));
            return CSharpMarkup.SystemXaml.DataTemplate.StartChain(ui);
        }

        public static ItemsPanelTemplate ItemsPanelTemplate(Func<UIElement> build) => ItemsPanelTemplate<Xaml.Controls.Grid>(build);

        public static ItemsPanelTemplate ItemsPanelTemplate(Action<Xaml.DependencyObject> build) => ItemsPanelTemplate<Xaml.Controls.Grid>(build);

        public static ItemsPanelTemplate ItemsPanelTemplate<TRootUI>(Func<UIElement> build) where TRootUI : Xaml.Controls.Panel, new()
        {
            var ui = (Xaml.Controls.ItemsPanelTemplate)CreateTemplate(nameof(Xaml.Controls.ItemsPanelTemplate), typeof(TRootUI), false, BuildChild.CreateIdFor(build));
            return CSharpMarkup.SystemXaml.ItemsPanelTemplate.StartChain(ui);
        }

        public static ItemsPanelTemplate ItemsPanelTemplate<TRootUI>(Action<Xaml.DependencyObject> build) where TRootUI : Xaml.UIElement, new()
        {
            // Note that we cannot pass markup objects to the build action here, because we get the ui type instance.
            var ui = (Xaml.Controls.ItemsPanelTemplate)CreateTemplate(nameof(Xaml.Controls.ItemsPanelTemplate), typeof(TRootUI), true, ConfigureRoot.CreateIdFor(build));            
            return CSharpMarkup.SystemXaml.ItemsPanelTemplate.StartChain(ui);
        }
        static object CreateTemplate(string templateTypeName, Type rootUIType, bool isConfigureRootDelegator, string delegatorId, Type targetType = null)
        {
            string xaml =
                $@"<{templateTypeName}{(targetType is null ? "" : $" TargetType=\"{targetType.Name}\"")}
					xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
					xmlns:x= ""http://schemas.microsoft.com/winfx/2006/xaml""
					xmlns:root=""using:{rootUIType.Namespace}""
                    xmlns:delegators=""using:{typeof(BuildChild).Namespace}"">
                    <root:{rootUIType.Name} delegators:{(isConfigureRootDelegator ? "ConfigureRoot" : "BuildChild")}.Id=""{SecurityElement.Escape(delegatorId)}"" />
				</{templateTypeName}>";

            return Xaml.Markup.XamlReader.Load(xaml);
        }

        public static ControlTemplate ControlTemplate(Func<UIElement> build) => ControlTemplate(null, build);

        public static ControlTemplate ControlTemplate(Type targetType, Func<UIElement> build)
        {
            var ui = (Xaml.Controls.ControlTemplate)CreateControlTemplate(nameof(Xaml.Controls.ControlTemplate), typeof(Controls.Grid), false, BuildChild.CreateIdFor(build), targetType);
            return CSharpMarkup.SystemXaml.ControlTemplate.StartChain(ui);
        }

        public static ControlTemplate ControlTemplate(Action<Xaml.DependencyObject> build) => ControlTemplate(null, build);

        public static ControlTemplate ControlTemplate(Type targetType, Action<Xaml.DependencyObject> build)
        {
            var ui = (Xaml.Controls.ControlTemplate)CreateControlTemplate(nameof(Xaml.Controls.ControlTemplate), typeof(Controls.Grid), true, ConfigureRoot.CreateIdFor(build), targetType);
            return CSharpMarkup.SystemXaml.ControlTemplate.StartChain(ui);
        }

        static object CreateControlTemplate(string templateTypeName, Type rootUIType, bool isConfigureRootDelegator, string delegatorId, Type targetType = null)
        {
            string bindTemplatedParent = @"{Binding RelativeSource={RelativeSource TemplatedParent}}";
            string delegatorTypeName = isConfigureRootDelegator ? nameof(ConfigureRootInControlTemplate) : nameof(BuildChildInControlTemplate);
            string xaml =
                $@"<{templateTypeName}{(targetType is null ? "" : $" TargetType=\"{targetType.Name}\"")}
					xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
					xmlns:x= ""http://schemas.microsoft.com/winfx/2006/xaml""
					xmlns:root=""using:{rootUIType.Namespace}""
                    xmlns:delegators=""using:{typeof(BuildChild).Namespace}"">
                    <root:{rootUIType.Name} delegators:{delegatorTypeName}.Id=""{SecurityElement.Escape(delegatorId)}"" delegators:{delegatorTypeName}.TemplatedParent=""{bindTemplatedParent}"" />
				</{templateTypeName}>";

            return Xaml.Markup.XamlReader.Load(xaml);
        }
    }
}