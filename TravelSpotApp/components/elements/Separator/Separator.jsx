import { Text } from "react-native";

import { StyledSeparatorWrapper, StyledSeparatorContent, StyledSeparatorLine } from "./styledSeparator";

const StyledSeparator = ({text}) => {
    return(
        <StyledSeparatorWrapper>
            <StyledSeparatorLine/>
            <StyledSeparatorContent style={{fontFamily: 'Urbanist_600SemiBold'}}>
                {text}
            </StyledSeparatorContent>
            <StyledSeparatorLine/>
        </StyledSeparatorWrapper>
    )
}

export default StyledSeparator;