cmake_minimum_required(VERSION 3.28)

project(ObsAi.ObsPlugin VERSION 0.1.0 LANGUAGES CXX)

if(CMAKE_INSTALL_PREFIX_INITIALIZED_TO_DEFAULT)
  set(CMAKE_INSTALL_PREFIX "${CMAKE_BINARY_DIR}/install" CACHE PATH "Safe local staging prefix" FORCE)
endif()

option(OBS_AI_BUILD_TESTS "Build deterministic native lifecycle tests" ON)
option(OBS_AI_BUILD_PLUGIN "Build the OBS module" ON)

if(OBS_AI_BUILD_PLUGIN)
  set(OBS_TARGET_VERSION "" CACHE STRING "Exact OBS package version used for this build")
  if(NOT OBS_TARGET_VERSION)
    message(FATAL_ERROR "Set OBS_TARGET_VERSION to the exact OBS version, for example 32.2.2")
  endif()

  find_package(libobs ${OBS_TARGET_VERSION} EXACT CONFIG REQUIRED)
  find_package(obs-frontend-api ${OBS_TARGET_VERSION} EXACT CONFIG REQUIRED)

  add_library(obs-ai-live-assistant MODULE
    src/module_lifecycle.cpp
    src/obs_frontend_adapter.cpp
    src/plugin_main.cpp
  )

  target_compile_features(obs-ai-live-assistant PRIVATE cxx_std_17)
  target_include_directories(obs-ai-live-assistant PRIVATE include)
  target_link_libraries(obs-ai-live-assistant PRIVATE OBS::libobs OBS::obs-frontend-api)
  target_compile_definitions(obs-ai-live-assistant PRIVATE WIN32_LEAN_AND_MEAN NOMINMAX)
  set_target_properties(obs-ai-live-assistant PROPERTIES
    PREFIX ""
    OUTPUT_NAME "obs-ai-live-assistant"
  )

  if(MSVC)
    target_compile_options(obs-ai-live-assistant PRIVATE /W4 /permissive- /EHsc)
  endif()
endif()

if(OBS_AI_BUILD_TESTS)
  enable_testing()
  add_executable(obs-ai-lifecycle-tests
    tests/module_lifecycle_tests.cpp
    src/module_lifecycle.cpp
  )
  target_compile_features(obs-ai-lifecycle-tests PRIVATE cxx_std_17)
  target_include_directories(obs-ai-lifecycle-tests PRIVATE include)
  if(MSVC)
    target_compile_options(obs-ai-lifecycle-tests PRIVATE /W4 /permissive- /EHsc)
  endif()
  add_test(NAME obs-ai-lifecycle COMMAND obs-ai-lifecycle-tests)
endif()
