@description('Location of all resources')
param location string = resourceGroup().location
@description('Generated from /subscriptions/13a61884-1a94-4e32-87a7-dc2267b9c957/resourceGroups/EventHubsRG/providers/Microsoft.EventHub/namespaces/eventhubabc')
resource eventhubabc 'Microsoft.EventHub/namespaces@2022-01-01-preview' = {
  sku: {
    name: 'Standard'
    tier: 'Standard'
    capacity: 8
  }
  name: 'eventhubabc'
  location: 'Australia East'
  tags: {
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
