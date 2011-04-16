MODULE jm_globals

! This module contains definition of the global variables and types 
! used in program, and some methods for the derived types

USE jm_constants

! Type for storing projectile properties
TYPE projectile_type
  REAL(KIND=dp) :: e     ! Energy
  REAL(KIND=dp) :: k     ! Wave vector
  INTEGER       :: l     ! Orbital angular quantum number
  INTEGER       :: kappa ! Relativistic quantum number
END TYPE

! Type for storing scattering potential parameters
TYPE potential_type
  CHARACTER(len=8) :: type  ! Type of potential
  REAL(KIND=dp)    :: r0    ! Truncating parameter
  REAL(KIND=dp)    :: v0    ! Depth of square-well
  REAL(KIND=dp)    :: a     ! Left bound of square-well
  REAL(KIND=dp)    :: b     ! Right bound of square-well
  REAL(KIND=dp)    :: z     ! Parameter of Coulomb potential
  REAL(KIND=dp)    :: alpha ! Parameter of Coulomb potential
  REAL(KIND=dp)    :: g     ! Parameter of Yukawa potential
  REAL(KIND=dp)    :: m     ! Parameter of Yukawa potential
END TYPE

TYPE (potential_type),  SAVE :: potential
TYPE (projectile_type), SAVE :: projectile
CHARACTER (LEN=8),      SAVE :: basis, vol
CHARACTER (LEN=16),     SAVE :: scheme
REAL(KIND=dp),          SAVE :: lambda
REAL(KIND=dp),          SAVE :: v_light
REAL(KIND=dp),          SAVE :: estart, eend, estep
INTEGER,                SAVE :: nstart, nend, ntrunc, pass
INTEGER,                SAVE :: ii, jj 
LOGICAL,                SAVE :: energies, scr, shift, delta_analytic_error
LOGICAL,                SAVE :: vn_is_loaded=.false., vn_file_match=.true.


CONTAINS
 

  SUBROUTINE projectile_set_k(this)

  ! Calculates and sets wave vector of projectile

    IMPLICIT none

    TYPE (projectile_type), INTENT(in out) :: this

    SELECT CASE(scheme)
    CASE('relativistic')

      this%k = sqrt(this%e * (this%e + two * v_light**2 ) ) / v_light

    CASE('non-relativistic')

      this%k = sqrt( two * this%e )

    END SELECT

  END SUBROUTINE projectile_set_k


END MODULE jm_globals
