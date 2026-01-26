using System.Collections.Generic;
using UnityEngine;

#if VIROO
using Viroo.Interactions;
#endif


namespace Isostopy.StepSystem.Viroo
{

/// <summary>
/// Componente que mantiene una cadena de pasos sincronizada para todos los usuarios de Viroo.
/// </summary>

#if VIROO
	[RequireComponent(typeof(StepChain))]
	public class VirooStepChain : BroadcastObjectAction
#else
	[RequireComponent(typeof(StepChain))]
	public class VirooStepChain : MonoBehaviour
#endif
	{

#if VIROO

		StepChain chain = null;
		Dictionary<Step, bool> stepsState = new();


		// --------------------------------------------------------------------------------

		private void Start()
		{
			chain = GetComponent<StepChain>();

			// Añadir todos los pasos al diciconario.
			var steps = chain.GetComponentsInChildren<Step>(true);
			foreach (var step in steps)
			{
				stepsState.Add(step, false);
			}
		}

		private void Update()
		{
			// Comprobamos si este frame ha terminado algun paso.
			var steps = new List<Step>(stepsState.Keys);
			foreach (var step in steps)
			{
				if (step.active != stepsState[step])
				{
					if (step.active == false)
					{
						OnStepEnded(step);
					}
					stepsState[step] = step.active;
				}
			}
		}

		private void OnStepEnded(Step step)
		{
			var index = chain.GetIndexOfStep(step);
			Execute(index.ToString());
		}

		protected override void LocalExecuteImplementation(string indexAsString)
		{
			var indexThatEnded = int.Parse(indexAsString);
			var stepThatEnded = chain.GetStepAtIndex(indexThatEnded);
			var currentIndex = chain.currentStepIndex;

			// Si vamos por delante del paso que ha terminado, nada.
			if (currentIndex > indexThatEnded)
			{
				// Nada
			}
			// Si vamos por un paso anterior.
			else if (currentIndex < indexThatEnded)
			{
				// Quiza haya que ir al que esta justo despues del que ha temriado.
				// Pero no se si esto puede pasar.
				Debug.Log("QUEEE??? Vamos por detras de lo que van los demas! No pensaba que esto fuera posible.");
			}
			// Si estamos en el mismo que ha terminado, avanzar.
			else if (currentIndex == indexThatEnded)
			{
				stepThatEnded.End();
			}
		}

#endif

	}
}
