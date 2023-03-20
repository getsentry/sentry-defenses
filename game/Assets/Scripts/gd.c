#ifdef _WIN32
#    define NOINLINE __declspec(noinline)
#else
#    define NOINLINE __attribute__((noinline))
#endif

NOINLINE void crashing_to_make_a_point()
{
    char *ptr = 0;
    *ptr += 1;
}
