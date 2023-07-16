import { Text } from "react-native";

import { StyledSeparatorWrapper, StyledSeparatorContent, StyledSeparatorLine } from "./styledSeparator";

const Separator = ({text}) => {
    return(
        <StyledSeparatorWrapper>
            <StyledSeparatorLine/>
            <StyledSeparatorContent>
                {text}
            </StyledSeparatorContent>
            <StyledSeparatorLine/>
        </StyledSeparatorWrapper>
    )
}

export default Separator;