cmake_minimum_required(VERSION 3.8.2)
project(RealCity VERSION 0.1.0 LANGUAGES CSharp)

file(GLOB_RECURSE SRCS *.cs)
file(GLOB_RECURSE RES Resources/*)
file(GLOB_RECURSE ICONS Icon/*)

add_library(RealCity ${SRCS})

set(REFS
    System
    System.Core
    System.Xml.Linq
    System.Data.DataSetExtensions
    System.Data
    System.Net.Http
    System.Xml
    )

set(CITIES_REFS
    Assembly-CSharp
    Assembly-CSharp-firstpass
    ColossalManaged
    ICities
    ICSharpCode.SharpZipLib
    Mono.Posix
    Mono.Security
    UnityEngine
    UnityEngine.Networking
    UnityEngine.UI
)

set(REFS ${REFS} ${CITIES_REFS} 0Harmony)
set_property(TARGET RealCity PROPERTY VS_DOTNET_REFERENCES ${REFS})

foreach(CITIES_REF IN ITEMS ${CITIES_REFS})
	set_target_properties(RealCity PROPERTIES VS_DOTNET_REFERENCE_${CITIES_REF} D:/Managed/${CITIES_REF}.dll)
endforeach()


find_program(NUGET nuget)
add_custom_target(nuget-restore
  COMMAND ${NUGET} restore ${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_NAME}.sln
)

configure_file(${CMAKE_CURRENT_SOURCE_DIR}/packages.config
${CMAKE_CURRENT_BINARY_DIR}/packages.config COPYONLY)

set_target_properties(${PROJECT_NAME} PROPERTIES
  VS_GLOBAL_ROOTNAMESPACE ${PROJECT_NAME}
  VS_GLOBAL_ProjectGuid "{40017200-7278-44D7-A12C-8DDC6BD98440}"
  VS_GLOBAL_CodeAnalysisRuleSet RealCity.ruleset)
    
target_compile_options(${PROJECT_NAME} PRIVATE "/langversion:6")

add_dependencies(${PROJECT_NAME} nuget-restore)

set_property(TARGET RealCity PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v3.5")
set_property(TARGET RealCity PROPERTY VS_DOTNET_REFERENCES_COPY_LOCAL False)

add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy Resources ${PROJECT_BINARY_DIR})
add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy Icon ${PROJECT_BINARY_DIR})
