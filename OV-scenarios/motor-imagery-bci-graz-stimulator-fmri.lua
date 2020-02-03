
function initialize(box)

--	dofile(box:get_config("${Player_ScenarioDirectory}") .. "/lua-stimulator-stim-codes.lua")
--	dofile(box:get_config("/lua-stimulator-stim-codes.lua"))
	dofile(box:get_config("${Path_Data}") .. "/plugins/stimulation/lua-stimulator-stim-codes.lua")

	number_of_repetitions = 3
	inter_trial_duration = 14 --rest

	number_of_trials = box:get_setting(2)
	first_class = _G[box:get_setting(3)]
	second_class = _G[box:get_setting(4)]
	baseline_duration = box:get_setting(5)
	wait_for_beep_duration = box:get_setting(6)
	wait_for_cue_duration = box:get_setting(7)
	display_cue_duration = box:get_setting(8)
	feedback_duration = box:get_setting(9)
	end_of_trial_min_duration = box:get_setting(10)
	end_of_trial_max_duration = box:get_setting(11)

	-- initializes random seed
	math.randomseed(os.time())

	-- fill the sequence table with predifined order
	sequence = {}

	for i = 1, number_of_trials do
		table.insert(sequence, 1, first_class)
		table.insert(sequence, 1, second_class)
	end	

	sequence_a = {1,2,3,4}
	sequence_b = {5,6,7,8}


end

-- chack if table contais stim (new)
local function has_value (tab, val)
    for index, value in ipairs(tab) do
        if value == val then
            return true
        end
    end

    return false
end


function process(box)

	local t=0

	-- manages baseline

	box:send_stimulation(1, OVTK_StimulationId_ExperimentStart, t, 0)
	t = t + 0

	box:send_stimulation(1, OVTK_StimulationId_BaselineStart, t, 0)
	box:send_stimulation(1, OVTK_StimulationId_Beep, t, 0)
	t = t + baseline_duration

	box:send_stimulation(1, OVTK_StimulationId_BaselineStop, t, 0)
	box:send_stimulation(1, OVTK_StimulationId_Beep, t, 0)
	t = t + 0

	-- manages trials
for j = 1, number_of_repetitions do

	for i = 1, number_of_trials do
		-- first display cross on screen

		box:send_stimulation(1, OVTK_GDF_Start_Of_Trial, t, 0)
		box:send_stimulation(1, OVTK_GDF_Cross_On_Screen, t, 0)
		--t = t + wait_for_beep_duration

		-- warn the user the cue is going to appear

		--box:send_stimulation(1, OVTK_StimulationId_Beep, t, 0)
		t = t + wait_for_cue_duration

		-- display cue

		-- box:send_stimulation(1, sequence[i], t, 0)
		if has_value(sequence_a, i) then
			box:send_stimulation(1, OVTK_GDF_Left, t, 0)
			t = t + display_cue_duration
		elseif has_value(sequence_b, i) then
			box:send_stimulation(1, OVTK_GDF_Right, t, 0)
			t = t + display_cue_duration
		end
		
		-- provide feedback

		box:send_stimulation(1, OVTK_GDF_Feedback_Continuous, t, 0)
		t = t + feedback_duration

		-- ends trial

		box:send_stimulation(1, OVTK_GDF_End_Of_Trial, t, 0)
		t = t + end_of_trial_max_duration
		--t = t + math.random(end_of_trial_min_duration, end_of_trial_max_duration)

	end
	
	--rest duration
	t = t + inter_trial_duration
	
end
	-- send end for completeness
	box:send_stimulation(1, OVTK_GDF_End_Of_Session, t, 0)
	t = t + 5

	box:send_stimulation(1, OVTK_StimulationId_Train, t, 0)
	t = t + 1
	
	-- used to cause the acquisition scenario to stop
	box:send_stimulation(1, OVTK_StimulationId_ExperimentStop, t, 0)

end
