import 'package:flutter/material.dart';
import 'package:travelspot/consts.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'TravelSpot',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: const MyHomePage(title: 'TravelSpot'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {

  @override
  Widget build(BuildContext context) {

    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
      ),
      body: const Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.start,

          children: <Widget>[
            Text('Welcome to TravelSpot', style: TextStyle(color: Colors.black, fontSize: 20),),
            //Email input
            TextField(
              decoration:
                InputDecoration(
                  hintText: "Email or username"
                )
              ,),
            //Password input
            TextField(
              decoration: InputDecoration(
                hintText: "Password"
              ),
            ),
          ],
        ),
      ),
    );
  }
}
