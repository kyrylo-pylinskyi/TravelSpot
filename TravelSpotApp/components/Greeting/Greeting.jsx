import { useState } from "react";
import { Easing, Image } from "react-native";
import TextTicker from "react-native-text-ticker";

import { greetingList } from "../../utils/greetingList";
import icon from '../../assets/icon.png';

import StyledBtn from "../elements/Buttons/CustomButton";
import { StyledTitle, StyledTitleItalic } from "../elements/typography";

const Greeting = ({setCurrentPage}) => {
    let greetingText = greetingList[Math.floor(Math.random()*greetingList.length)];

    const submitAction = (action) => {
        setGreetingAction(action);
    }

    return(
        <>
            <Image
                source={icon}
                style={{width: 200, height: 200}}
            />
            <TextTicker
                style={{paddingBottom: 0}}
                scrollSpeed={16}
                loop
                repeatSpacer={10}
                easing={Easing.linear}
            >
                <StyledTitle style={{fontSize: 30}}>
                    {greetingText}
                </StyledTitle>
            </TextTicker>
            <TextTicker
                style={{paddingBottom: 0}}
                scrollSpeed={15}
                loop
                repeatSpacer={10}
                easing={Easing.linear}
            >
                <StyledTitleItalic style={{fontSize: 30}}>
                    {greetingText}
                </StyledTitleItalic>

            </TextTicker>
            <TextTicker
                style={{paddingBottom: 30}}
                scrollSpeed={17}
                loop
                repeatSpacer={10}
                easing={Easing.linear}
            >
                <StyledTitle style={{fontSize: 30}}>
                    {greetingText}
                </StyledTitle>
            </TextTicker>
            <StyledBtn 
                title="Login" 
                action={() => setCurrentPage('login')}
                type="primary"
            />
            <StyledBtn 
                title="Register" 
                action={() => setCurrentPage('registration')}
                type="secondary"
            />
        </>
    )
}

export default Greeting;