# JustMoby- Тестовое задание

Используемые плагины:

[![Unity](https://img.shields.io/badge/Unity-20223.61f1%2B-blue.svg)](https://unity3d.com)
[![MessagePipe](https://img.shields.io/badge/MessagePipe-1.7%2B-green.svg)](https://github.com/Cysharp/MessagePipe)
[![DiMessageBus](https://img.shields.io/badge/DiMessageBus-1.0.1%2B-green.svg)](https://github.com/irisss7777/DiMessageBus)
[![Zenject](https://img.shields.io/badge/Zenject-9.2%2B-orange.svg)](https://github.com/modesttree/Zenject)


## 🎮 Видео-демонстрация - https://drive.google.com/file/d/1egeUJ2U42rF2GVNRA-hCbPm9kWKzDdAY/view?usp=sharing

## 📋 Запуск
  - Открыть проект
  - Открыть Assets\Content\Scenes\SampleScene
  - Запустить игру

## 🏗 Архитектурные решения

# 1. Domain-Driven Design (DDD)

Проект построен с использованием принципов предметно-ориентированного проектирования (DDD). Основные слои:

  - Domain (Домен): Содержит бизнес-логику, сущности, value-объекты.
  - Application (Приложение): Реализует use cases и services, координирует работу доменных моделей с помощью сервисов и команд.
  - Infrastructure (Инфраструктура): Отвечает за фабрики, реализацию репозиториев, а также интеграцию с шиной событий и DI-контейнером.
  - Presentation (Представление): Содержит UI-контроллеры и визуальные компоненты, которые подписываются на события и отображают состояние.

# 2. Реактивность через шину событий (MessagePipe + DiMessageBus)

Вместо традиционного подхода с UniRx и ReactiveProperty для организации реактивных связей выбрана шина событий на базе MessagePipe и собственной обёртки DiMessageBus.

# 3. DI (Zenject)

Весь проект построен на внедрении зависимостей через Zenject. Инсталлеры разделены по слоям, что позволяет легко подменять реализации при тестировании или расширении функциональности.

Assets/
├── Content/
│   ├── Application/       # Use cases, Service
│   ├── Contracts/         # Интерфейсы-прокладки(разделены на Application contracts и Presentation contracts)
│   ├── Domain/            # Модели
│   ├── Infrastructure/    # Database, Factory, Installers, Bootstrapper
│   ├── Presentation/      # UI, View, Presenters, подписчики событий
│   └── Utils/             # Утилити Service и UseCase
└── ...
