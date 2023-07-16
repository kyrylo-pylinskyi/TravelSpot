import { StyleSheet, View, Text } from 'react-native';
import MapView from 'react-native-maps';
import * as Location from 'expo-location';
import { useState, useEffect } from 'react';


import { StyledMapView, StyledMapViewMenu, StyledMapWrapper } from './styledMap';

const Map = () => {

    const [location, setLocation] = useState(null);
    const [errorMsg, setErrorMsg] = useState(null);

    useEffect(() => {
        (async () => {
          
          let { status } = await Location.requestForegroundPermissionsAsync();
          if (status !== 'granted') {
            setErrorMsg('Permission to access location was denied');
            return;
          }
    
          let location = await Location.getCurrentPositionAsync({});
          setLocation(location);
        })();
      }, []);
    
      let text = 'Waiting..';
      if (errorMsg) {
        text = errorMsg;
      } else if (location) {
        text = JSON.stringify(location);
      }

    return (
        <StyledMapView>
            <StyledMapWrapper>
                <MapView style={styles.map}/>
            </StyledMapWrapper>
            <StyledMapViewMenu>
                <Text style={styles.paragraph}>{text}</Text>
            </StyledMapViewMenu>
        </StyledMapView>
    )
}

const styles = StyleSheet.create({
    map: {
      width: '100%',
      height: '100%',
    },
});

export default Map;