if(NOT TARGET sentry-native-ndk::sentry)
add_library(sentry-native-ndk::sentry SHARED IMPORTED)
set_target_properties(sentry-native-ndk::sentry PROPERTIES
    IMPORTED_LOCATION "/Users/bitfox/.gradle/caches/transforms-3/30d59873124bf63893e4899d2205078a/transformed/jetified-sentry-native-ndk-release/prefab/modules/sentry/libs/android.arm64-v8a/libsentry.so"
    INTERFACE_INCLUDE_DIRECTORIES "/Users/bitfox/.gradle/caches/transforms-3/30d59873124bf63893e4899d2205078a/transformed/jetified-sentry-native-ndk-release/prefab/modules/sentry/include"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

if(NOT TARGET sentry-native-ndk::sentry-android)
add_library(sentry-native-ndk::sentry-android SHARED IMPORTED)
set_target_properties(sentry-native-ndk::sentry-android PROPERTIES
    IMPORTED_LOCATION "/Users/bitfox/.gradle/caches/transforms-3/30d59873124bf63893e4899d2205078a/transformed/jetified-sentry-native-ndk-release/prefab/modules/sentry-android/libs/android.arm64-v8a/libsentry-android.so"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

