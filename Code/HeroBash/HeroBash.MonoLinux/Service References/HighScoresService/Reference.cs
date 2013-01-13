﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeroBash.HighScoresService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Score", Namespace="http://schemas.datacontract.org/2004/07/HighScoresService", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Score : HighScoresService.EntityObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateSubmittedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> DecimalScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ExtraIntField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExtraStringField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> FloatScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GameIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IntScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlayerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> RankField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScoreIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateSubmitted {
            get {
                return this.DateSubmittedField;
            }
            set {
                if ((this.DateSubmittedField.Equals(value) != true)) {
                    this.DateSubmittedField = value;
                    this.RaisePropertyChanged("DateSubmitted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> DecimalScore {
            get {
                return this.DecimalScoreField;
            }
            set {
                if ((this.DecimalScoreField.Equals(value) != true)) {
                    this.DecimalScoreField = value;
                    this.RaisePropertyChanged("DecimalScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ExtraInt {
            get {
                return this.ExtraIntField;
            }
            set {
                if ((this.ExtraIntField.Equals(value) != true)) {
                    this.ExtraIntField = value;
                    this.RaisePropertyChanged("ExtraInt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExtraString {
            get {
                return this.ExtraStringField;
            }
            set {
                if ((object.ReferenceEquals(this.ExtraStringField, value) != true)) {
                    this.ExtraStringField = value;
                    this.RaisePropertyChanged("ExtraString");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> FloatScore {
            get {
                return this.FloatScoreField;
            }
            set {
                if ((this.FloatScoreField.Equals(value) != true)) {
                    this.FloatScoreField = value;
                    this.RaisePropertyChanged("FloatScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GameID {
            get {
                return this.GameIDField;
            }
            set {
                if ((this.GameIDField.Equals(value) != true)) {
                    this.GameIDField = value;
                    this.RaisePropertyChanged("GameID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> IntScore {
            get {
                return this.IntScoreField;
            }
            set {
                if ((this.IntScoreField.Equals(value) != true)) {
                    this.IntScoreField = value;
                    this.RaisePropertyChanged("IntScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlayerName {
            get {
                return this.PlayerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayerNameField, value) != true)) {
                    this.PlayerNameField = value;
                    this.RaisePropertyChanged("PlayerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Rank {
            get {
                return this.RankField;
            }
            set {
                if ((this.RankField.Equals(value) != true)) {
                    this.RankField = value;
                    this.RaisePropertyChanged("Rank");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ScoreID {
            get {
                return this.ScoreIDField;
            }
            set {
                if ((this.ScoreIDField.Equals(value) != true)) {
                    this.ScoreIDField = value;
                    this.RaisePropertyChanged("ScoreID");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StructuralObject", Namespace="http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.EntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.WeeklyScore))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.LastWeek))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.Score))]
    public partial class StructuralObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityObject", Namespace="http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.WeeklyScore))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.LastWeek))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.Score))]
    public partial class EntityObject : HighScoresService.StructuralObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HighScoresService.EntityKey EntityKeyField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HighScoresService.EntityKey EntityKey {
            get {
                return this.EntityKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityKeyField, value) != true)) {
                    this.EntityKeyField = value;
                    this.RaisePropertyChanged("EntityKey");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WeeklyScore", Namespace="http://schemas.datacontract.org/2004/07/HighScoresService", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class WeeklyScore : HighScoresService.EntityObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateSubmittedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> DecimalScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ExtraIntField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExtraStringField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> FloatScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GameIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IntScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlayerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> RankField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScoreIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateSubmitted {
            get {
                return this.DateSubmittedField;
            }
            set {
                if ((this.DateSubmittedField.Equals(value) != true)) {
                    this.DateSubmittedField = value;
                    this.RaisePropertyChanged("DateSubmitted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> DecimalScore {
            get {
                return this.DecimalScoreField;
            }
            set {
                if ((this.DecimalScoreField.Equals(value) != true)) {
                    this.DecimalScoreField = value;
                    this.RaisePropertyChanged("DecimalScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ExtraInt {
            get {
                return this.ExtraIntField;
            }
            set {
                if ((this.ExtraIntField.Equals(value) != true)) {
                    this.ExtraIntField = value;
                    this.RaisePropertyChanged("ExtraInt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExtraString {
            get {
                return this.ExtraStringField;
            }
            set {
                if ((object.ReferenceEquals(this.ExtraStringField, value) != true)) {
                    this.ExtraStringField = value;
                    this.RaisePropertyChanged("ExtraString");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> FloatScore {
            get {
                return this.FloatScoreField;
            }
            set {
                if ((this.FloatScoreField.Equals(value) != true)) {
                    this.FloatScoreField = value;
                    this.RaisePropertyChanged("FloatScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GameID {
            get {
                return this.GameIDField;
            }
            set {
                if ((this.GameIDField.Equals(value) != true)) {
                    this.GameIDField = value;
                    this.RaisePropertyChanged("GameID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> IntScore {
            get {
                return this.IntScoreField;
            }
            set {
                if ((this.IntScoreField.Equals(value) != true)) {
                    this.IntScoreField = value;
                    this.RaisePropertyChanged("IntScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlayerName {
            get {
                return this.PlayerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayerNameField, value) != true)) {
                    this.PlayerNameField = value;
                    this.RaisePropertyChanged("PlayerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Rank {
            get {
                return this.RankField;
            }
            set {
                if ((this.RankField.Equals(value) != true)) {
                    this.RankField = value;
                    this.RaisePropertyChanged("Rank");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ScoreID {
            get {
                return this.ScoreIDField;
            }
            set {
                if ((this.ScoreIDField.Equals(value) != true)) {
                    this.ScoreIDField = value;
                    this.RaisePropertyChanged("ScoreID");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LastWeek", Namespace="http://schemas.datacontract.org/2004/07/HighScoresService", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class LastWeek : HighScoresService.EntityObject {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateSubmittedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> DecimalScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ExtraIntField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExtraStringField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> FloatScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GameIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IntScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlayerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> RankField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScoreIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateSubmitted {
            get {
                return this.DateSubmittedField;
            }
            set {
                if ((this.DateSubmittedField.Equals(value) != true)) {
                    this.DateSubmittedField = value;
                    this.RaisePropertyChanged("DateSubmitted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> DecimalScore {
            get {
                return this.DecimalScoreField;
            }
            set {
                if ((this.DecimalScoreField.Equals(value) != true)) {
                    this.DecimalScoreField = value;
                    this.RaisePropertyChanged("DecimalScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ExtraInt {
            get {
                return this.ExtraIntField;
            }
            set {
                if ((this.ExtraIntField.Equals(value) != true)) {
                    this.ExtraIntField = value;
                    this.RaisePropertyChanged("ExtraInt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExtraString {
            get {
                return this.ExtraStringField;
            }
            set {
                if ((object.ReferenceEquals(this.ExtraStringField, value) != true)) {
                    this.ExtraStringField = value;
                    this.RaisePropertyChanged("ExtraString");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> FloatScore {
            get {
                return this.FloatScoreField;
            }
            set {
                if ((this.FloatScoreField.Equals(value) != true)) {
                    this.FloatScoreField = value;
                    this.RaisePropertyChanged("FloatScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GameID {
            get {
                return this.GameIDField;
            }
            set {
                if ((this.GameIDField.Equals(value) != true)) {
                    this.GameIDField = value;
                    this.RaisePropertyChanged("GameID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> IntScore {
            get {
                return this.IntScoreField;
            }
            set {
                if ((this.IntScoreField.Equals(value) != true)) {
                    this.IntScoreField = value;
                    this.RaisePropertyChanged("IntScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlayerName {
            get {
                return this.PlayerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayerNameField, value) != true)) {
                    this.PlayerNameField = value;
                    this.RaisePropertyChanged("PlayerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Rank {
            get {
                return this.RankField;
            }
            set {
                if ((this.RankField.Equals(value) != true)) {
                    this.RankField = value;
                    this.RaisePropertyChanged("Rank");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ScoreID {
            get {
                return this.ScoreIDField;
            }
            set {
                if ((this.ScoreIDField.Equals(value) != true)) {
                    this.ScoreIDField = value;
                    this.RaisePropertyChanged("ScoreID");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityKey", Namespace="http://schemas.datacontract.org/2004/07/System.Data", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class EntityKey : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EntityContainerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HighScoresService.EntityKeyMember[] EntityKeyValuesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EntitySetNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EntityContainerName {
            get {
                return this.EntityContainerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityContainerNameField, value) != true)) {
                    this.EntityContainerNameField = value;
                    this.RaisePropertyChanged("EntityContainerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HighScoresService.EntityKeyMember[] EntityKeyValues {
            get {
                return this.EntityKeyValuesField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityKeyValuesField, value) != true)) {
                    this.EntityKeyValuesField = value;
                    this.RaisePropertyChanged("EntityKeyValues");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EntitySetName {
            get {
                return this.EntitySetNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EntitySetNameField, value) != true)) {
                    this.EntitySetNameField = value;
                    this.RaisePropertyChanged("EntitySetName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityKeyMember", Namespace="http://schemas.datacontract.org/2004/07/System.Data")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.EntityObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.StructuralObject))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.EntityKey))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.EntityKeyMember[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.Score[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.Score))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.WeeklyScore[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.WeeklyScore))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.LastWeek[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HighScoresService.LastWeek))]
    public partial class EntityKeyMember : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key {
            get {
                return this.KeyField;
            }
            set {
                if ((object.ReferenceEquals(this.KeyField, value) != true)) {
                    this.KeyField = value;
                    this.RaisePropertyChanged("Key");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HighScoresService.IHeroBashHighScores")]
    public interface IHeroBashHighScores {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetNearbyScoresInGame", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetNearbyScoresInGameResponse")]
        HighScoresService.Score[] GetNearbyScoresInGame(int playthrough, int stage, float level, decimal time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetNearbyScores", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetNearbyScoresResponse")]
        HighScoresService.Score[] GetNearbyScores(int scoreId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetTopTenScores", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetTopTenScoresResponse")]
        HighScoresService.Score[] GetTopTenScores();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetNearbyWeeklyScoresInGame", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetNearbyWeeklyScoresInGameResponse")]
        HighScoresService.WeeklyScore[] GetNearbyWeeklyScoresInGame(int playthrough, int stage, float level, decimal time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetNearbyWeeklyScores", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetNearbyWeeklyScoresResponse")]
        HighScoresService.WeeklyScore[] GetNearbyWeeklyScores(int scoreId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetTopTenWeeklyScores", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetTopTenWeeklyScoresResponse")]
        HighScoresService.WeeklyScore[] GetTopTenWeeklyScores();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetLastWeekScore", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetLastWeekScoreResponse")]
        HighScoresService.LastWeek[] GetLastWeekScore();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetMyPreviousScores", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetMyPreviousScoresResponse")]
        HighScoresService.Score[] GetMyPreviousScores(string playerid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetMyNearbyPreviousScoresInGame", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetMyNearbyPreviousScoresInGameResponse")]
        HighScoresService.Score[] GetMyNearbyPreviousScoresInGame(string playerid, int playthrough, int stage, float level, decimal time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/GetMyNearbyPreviousScores", ReplyAction="http://tempuri.org/IHeroBashHighScores/GetMyNearbyPreviousScoresResponse")]
        HighScoresService.Score[] GetMyNearbyPreviousScores(string playerid, int scoreId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/AddScore", ReplyAction="http://tempuri.org/IHeroBashHighScores/AddScoreResponse")]
        int AddScore(string playername, string playerid, int playthrough, int stage, float level, decimal time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeroBashHighScores/AddWeeklyScore", ReplyAction="http://tempuri.org/IHeroBashHighScores/AddWeeklyScoreResponse")]
        int AddWeeklyScore(string playername, string playerid, int playthrough, int stage, float level, decimal time);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHeroBashHighScoresChannel : HighScoresService.IHeroBashHighScores, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HeroBashHighScoresClient : System.ServiceModel.ClientBase<HighScoresService.IHeroBashHighScores>, HighScoresService.IHeroBashHighScores {
        
        public HeroBashHighScoresClient() {
        }
        
        public HeroBashHighScoresClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HeroBashHighScoresClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HeroBashHighScoresClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HeroBashHighScoresClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public HighScoresService.Score[] GetNearbyScoresInGame(int playthrough, int stage, float level, decimal time) {
            return base.Channel.GetNearbyScoresInGame(playthrough, stage, level, time);
        }
        
        public HighScoresService.Score[] GetNearbyScores(int scoreId) {
            return base.Channel.GetNearbyScores(scoreId);
        }
        
        public HighScoresService.Score[] GetTopTenScores() {
            return base.Channel.GetTopTenScores();
        }
        
        public HighScoresService.WeeklyScore[] GetNearbyWeeklyScoresInGame(int playthrough, int stage, float level, decimal time) {
            return base.Channel.GetNearbyWeeklyScoresInGame(playthrough, stage, level, time);
        }
        
        public HighScoresService.WeeklyScore[] GetNearbyWeeklyScores(int scoreId) {
            return base.Channel.GetNearbyWeeklyScores(scoreId);
        }
        
        public HighScoresService.WeeklyScore[] GetTopTenWeeklyScores() {
            return base.Channel.GetTopTenWeeklyScores();
        }
        
        public HighScoresService.LastWeek[] GetLastWeekScore() {
            return base.Channel.GetLastWeekScore();
        }
        
        public HighScoresService.Score[] GetMyPreviousScores(string playerid) {
            return base.Channel.GetMyPreviousScores(playerid);
        }
        
        public HighScoresService.Score[] GetMyNearbyPreviousScoresInGame(string playerid, int playthrough, int stage, float level, decimal time) {
            return base.Channel.GetMyNearbyPreviousScoresInGame(playerid, playthrough, stage, level, time);
        }
        
        public HighScoresService.Score[] GetMyNearbyPreviousScores(string playerid, int scoreId) {
            return base.Channel.GetMyNearbyPreviousScores(playerid, scoreId);
        }
        
        public int AddScore(string playername, string playerid, int playthrough, int stage, float level, decimal time) {
            return base.Channel.AddScore(playername, playerid, playthrough, stage, level, time);
        }
        
        public int AddWeeklyScore(string playername, string playerid, int playthrough, int stage, float level, decimal time) {
            return base.Channel.AddWeeklyScore(playername, playerid, playthrough, stage, level, time);
        }
    }
}
