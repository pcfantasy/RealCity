cmake_minimum_required(VERSION 3.8.2)
project(RealCity VERSION 0.1.0 LANGUAGES CSharp)

file(GLOB_RECURSE SRCS *.cs)
file(GLOB_RECURSE RES Resources/*)
file(GLOB_RECURSE ICONS Icon/*)

set(CITIES_STEAM_ID 255710)
set(REAL_CITY_STEAM_ID 1192503086)

set(STEAM_CITIES_LIBRARY "C:/Program Files (x86)/Steam" CACHE PATH "Path to steam library which contents Cities:Skylines")
find_path(CITIES_MOD_PARENT_PATH 
          ${CITIES_STEAM_ID}
          PATHS "${STEAM_CITIES_LIBRARY}/steamapps/workshop/content"
          DOC "Path to RealCity installation")
		 
set(MOD_PATH "${CITIES_MOD_PARENT_PATH}/${CITIES_STEAM_ID}/${REAL_CITY_STEAM_ID}") 

add_library(${PROJECT_NAME} ${SRCS})

find_path(CITIES_MANAGED_PATH
	ICities.dll
	PATHS "${STEAM_CITIES_LIBRARY}/steamapps/common/Cities_Skylines/Cities_Data/Managed" DOC "Path to Cities Skylines managed libs")

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

set_property(TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_REFERENCES ${REFS})

foreach(CITIES_REF IN ITEMS ${CITIES_REFS})
	set_target_properties(${PROJECT_NAME} PROPERTIES VS_DOTNET_REFERENCE_${CITIES_REF} "${CITIES_MANAGED_PATH}/${CITIES_REF}.dll")
endforeach()

set_target_properties(${PROJECT_NAME} PROPERTIES VS_DOTNET_REFERENCE_0Harmony "${CMAKE_BINARY_DIR}/packages/Lib.Harmony.1.2.0.1/lib/net35/0Harmony.dll")


find_program(NUGET nuget HINTS "${CMAKE_SOURCE_DIR}")

add_custom_command(TARGET ${PROJECT_NAME}
    PRE_BUILD
    COMMAND "${NUGET}" restore "${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_NAME}.sln"
)

set_target_properties(${PROJECT_NAME} PROPERTIES
  VS_GLOBAL_ROOTNAMESPACE ${PROJECT_NAME}
  VS_GLOBAL_ProjectGuid "{40017200-7278-44D7-A12C-8DDC6BD98440}"
  VS_GLOBAL_CodeAnalysisRuleSet "${CMAKE_SOURCE_DIR}/RealCity.ruleset"
  VS_GLOBAL_FileAlignment "512"
  VS_GLOBAL_Prefer32Bit "false"
  VS_GLOBAL_LangVersion "6"
  VS_GLOBAL_AllowUnsafeBlocks "true"
  VS_GLOBAL_Platform "AnyCPU"
  VS_GLOBAL_WarningLevel "4"
  VS_GLOBAL_PlatformTarget "AnyCPU"
  )
    
target_compile_options(${PROJECT_NAME} PRIVATE "/langversion:6" "/unsafe" "/platform:anycpu")


set_property(TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v3.5")
set_property(TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_REFERENCES_COPY_LOCAL False)

execute_process(COMMAND "${NUGET}" install "${CMAKE_SOURCE_DIR}/packages.config" -OutputDirectory "${CMAKE_BINARY_DIR}/packages" -SolutionDirectory "${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_NAME}") 

add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD COMMAND "${CMAKE_COMMAND}" -E copy_directory "${CMAKE_SOURCE_DIR}/Resources" "${MOD_PATH}/Resources")
add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD COMMAND "${CMAKE_COMMAND}" -E copy_directory "${CMAKE_SOURCE_DIR}/Icon" "${MOD_PATH}/Icon")
add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD COMMAND "${CMAKE_COMMAND}" -E copy_if_different "${CMAKE_BINARY_DIR}/${CMAKE_CFG_INTDIR}/${PROJECT_NAME}.dll" "${MOD_PATH}")
add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD COMMAND "${CMAKE_COMMAND}" -E copy_if_different "${CMAKE_BINARY_DIR}/packages/Lib.Harmony.1.2.0.1/lib/net35/0Harmony.dll" "${MOD_PATH}/0Harmony.dll")

