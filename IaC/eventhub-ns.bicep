param namespaces_eventhubabc_name string = 'eventhubabc'

resource namespaces_eventhubabc_name_resource 'Microsoft.EventHub/namespaces@2022-01-01-preview' = {
  name: namespaces_eventhubabc_name
  location: 'Australia East'
  sku: {
    name: 'Standard'
    tier: 'Standard'
    capacity: 8
  }
  properties: {
    minimumTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    disableLocalAuth: false
    zoneRedundant: true
    isAutoInflateEnabled: false
    maximumThroughputUnits: 0
    kafkaEnabled: true
  }
}

resource namespaces_eventhubabc_name_RootManageSharedAccessKey 'Microsoft.EventHub/namespaces/AuthorizationRules@2022-01-01-preview' = {
  parent: namespaces_eventhubabc_name_resource
  name: 'RootManageSharedAccessKey'
  location: 'Australia East'
  properties: {
    rights: [
      'Listen'
      'Manage'
      'Send'
    ]
  }
}

resource namespaces_eventhubabc_name_eventhubtopic1 'Microsoft.EventHub/namespaces/eventhubs@2022-01-01-preview' = {
  parent: namespaces_eventhubabc_name_resource
  name: 'eventhubtopic1'
  location: 'australiaeast'
  properties: {
    messageRetentionInDays: 1
    partitionCount: 8
    status: 'Active'
  }
}

resource namespaces_eventhubabc_name_default 'Microsoft.EventHub/namespaces/networkRuleSets@2022-01-01-preview' = {
  parent: namespaces_eventhubabc_name_resource
  name: 'default'
  location: 'Australia East'
  properties: {
    publicNetworkAccess: 'Enabled'
    defaultAction: 'Allow'
    virtualNetworkRules: []
    ipRules: []
  }
}

resource namespaces_eventhubabc_name_eventhubtopic1_PreviewDataPolicy 'Microsoft.EventHub/namespaces/eventhubs/authorizationrules@2022-01-01-preview' = {
  parent: namespaces_eventhubabc_name_eventhubtopic1
  name: 'PreviewDataPolicy'
  location: 'australiaeast'
  properties: {
    rights: [
      'Listen'
    ]
  }
  dependsOn: [

    namespaces_eventhubabc_name_resource
  ]
}

resource namespaces_eventhubabc_name_eventhubtopic1_Default 'Microsoft.EventHub/namespaces/eventhubs/consumergroups@2022-01-01-preview' = {
  parent: namespaces_eventhubabc_name_eventhubtopic1
  name: '$Default'
  location: 'australiaeast'
  properties: {
    userMetadata: '$Default'
  }
  dependsOn: [

    namespaces_eventhubabc_name_resource
  ]
}

resource namespaces_eventhubabc_name_eventhubtopic1_preview_data_consumer_group 'Microsoft.EventHub/namespaces/eventhubs/consumergroups@2022-01-01-preview' = {
  parent: namespaces_eventhubabc_name_eventhubtopic1
  name: 'preview_data_consumer_group'
  location: 'australiaeast'
  properties: {
  }
  dependsOn: [

    namespaces_eventhubabc_name_resource
  ]
}
