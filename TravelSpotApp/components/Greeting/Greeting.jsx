import { useState } from "react";
import { Easing } from "react-native";
import TextTicker from "react-native-text-ticker";

import { greetingList } from "../../utils/greetingList";

import StyledBtn from "../elements/Buttons/CustomButton";
import { StyledTitle, StyledTitleItalic } from "../elements/typography";

const Greeting = ({setCurrentPage}) => {
    let greetingText = greetingList[Math.floor(Math.random()*greetingList.length)];

    const submitAction = (action) => {
        setGreetingAction(action);
    }

    return(
        <>
            <TextTicker
                style={{paddingBottom: 0}}
                scrollSpeed={16}
                loop
                repeatSpacer={10}
                easing={Easing.linear}
            >
                <StyledTitle>
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
                <StyledTitleItalic>
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
                <StyledTitle>
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