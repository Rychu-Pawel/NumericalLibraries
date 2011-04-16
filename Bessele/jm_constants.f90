MODULE jm_constants

! Definitions of constants used in the JMATRIX program

!INTEGER, PARAMETER :: i8b = selected_int_kind(18)
INTEGER, PARAMETER :: i4b = selected_int_kind(9)
INTEGER, PARAMETER :: i2b = selected_int_kind(4)
INTEGER, PARAMETER :: i1b = selected_int_kind(2)

INTEGER, PARAMETER :: sp = selected_real_kind(6,37)
INTEGER, PARAMETER :: dp = selected_real_kind(15,307)

REAL(KIND=dp), PARAMETER :: &
                 pi    = 3.141592653589793238462643383279502884197_dp
REAL(KIND=dp), PARAMETER :: &
                 twopi = 6.283185307179586476925286766559005768394_dp

REAL(KIND=dp), PARAMETER :: zero  =  0.0_dp
REAL(KIND=dp), PARAMETER :: one   =  1.0_dp
REAL(KIND=dp), PARAMETER :: two   =  2.0_dp
REAL(KIND=dp), PARAMETER :: three =  3.0_dp
REAL(KIND=dp), PARAMETER :: four  =  4.0_dp
REAL(KIND=dp), PARAMETER :: five  =  5.0_dp
REAL(KIND=dp), PARAMETER :: six   =  6.0_dp
REAL(KIND=dp), PARAMETER :: seven =  7.0_dp
REAL(KIND=dp), PARAMETER :: eight =  8.0_dp
REAL(KIND=dp), PARAMETER :: nine  =  9.0_dp
REAL(KIND=dp), PARAMETER :: ten   = 10.0_dp

REAL(KIND=dp), PARAMETER :: half       = one/two
REAL(KIND=dp), PARAMETER :: quarter    = one/four
REAL(KIND=dp), PARAMETER :: oneandhalf = one+half
REAL(KIND=dp), PARAMETER :: twoandhalf = two+half

END MODULE jm_constants
