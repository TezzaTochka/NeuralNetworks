Я представляю три нейросети, написанные с нуля без библиотек.

Первая нейросеть(NumberPredictor)написана на языке c++, имеет алгоритм обучения обратного распространения ошибки(BackPropogation), а также она сверточная. 
Данная нейросеть распознает рукописные цифры. Имеется learningmodel, которая обучается распозновать цифры, и resultingmodel - это уже обученная нейросеть, 
которая готова распозновать рукописные цифры. 
Также надо сказать, что когда вы будете пробовать рисовать цифры в resultingmodel, то точность с которой она будет отгадывать ваши цифры, 
будет сильно занижена, из-за того, что у рукописных цифр, написанных компьютерной мышкой, в отличии от тех, которые были написаны на листе бумаги ручкой, 
много различий, к которым первая нейросеть не обучалась.
путь к exe файлу(NumberPredictor-learningmodel) - NumberPredictor-learningmodel\x64\Debug\NumberPredictor-learningmodel.exe
путь к exe файлу(NumberPredictor-resultingmodel) - NumberPredictor-resultingmodel\x64\Debug\NumberPredictor-resultingmodel.exe
(если будут проблемы с библиотекой SFML по выводу графического интерфейса, то посмотрите этот туториал https://www.youtube.com/watch?v=w339OWGlSo0)

Вторая нейросеть(Pong)написана на языке c#, имеет генетический алгоритм обучения. В learningmodel вы можете ускорять время обучения, нажимая клавишу(+), 
и замедлять время, нажимая клавишу(-). Также вы можете пропускать некоторое количество итераций(тем самым, ускоряя время обучения), нажимая клавишу(]), 
для обратного эффекта вам надо нажимать клавишу([). В resultingmodel представлена уже обученная нейросеть, где вы также можете ускорять и замедлять время работы.
путь к exe файлу(Pong-learningmodel) - Pong-learningmodel\NeuralNetworkPong\publish\Pong-learningmodel.exe
путь к exe файлу(Pong-resultingmodel) - Pong-resultingmodel\JustPong\publish\Pong-resultingmodel.exe

Третья нейросеть(Snake)написаня на языке c#, имеет генетический алгоритм обучения. В learningmodel вы можете ускорять время обучения, нажимая клавишу(+), 
и замедлять время, нажимая клавишу(-). Также вы можете пропускать некоторое количество итераций(тем самым ускоряя время обучения), нажимая клавишу(]), 
для обратного эффекта вам надо нажимать клавишу([). В resultingmodel представлена уже обученная нейросеть, где вы также можете ускорять и замедлять время работы.
путь к exe файлу(Snake-learningmodel) - Snake-learningmodel\GeneticAlgorithmSnake\publish\Snake-learningmodel.exe
путь к exe файлу(Snake-resultingmodel) - Snake-resultingmodel\JustSnake\publish\Snake-resultingmodel.exe
