import AuthPage from './pages/AuthPage/AuthPage';
import { MainView } from './components/style';
import { Text } from 'react-native';
import { useFonts, Urbanist_400Regular, Urbanist_500Medium, Urbanist_600SemiBold, Urbanist_700Bold} from '@expo-google-fonts/urbanist'

export default function App() {

  let [fontsLoaded] = useFonts({
    Urbanist_400Regular,
    Urbanist_600SemiBold,
    Urbanist_700Bold,
    Urbanist_500Medium
  });

  if (!fontsLoaded) {
    return null;
  }

  return (
    <MainView>
      <AuthPage/>
    </MainView>
    );
}